using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class QuitarContasPàgarControl
    {

        public bool PagarTotal(int codigo)
        {
            Models.ContasPagar Conta = new DAL.QuitarContasPagarDAL().ObterDespesa(codigo);
            
            DAL.QuitarContasPagarDAL dal = new DAL.QuitarContasPagarDAL();

            return dal.PagarTotal(Conta);
        }

        public bool PagarParcial(int codigo,decimal valorpago,DateTime datavenc)
        {
            if(valorpago>0)
            {
                Models.ContasPagar Conta = new DAL.QuitarContasPagarDAL().ObterDespesa(codigo);
                Conta.Valor = Conta.Valor - valorpago;
                Conta.Valorpago = 0;
                Conta.Contamae = Conta.Codigo;
                Conta.Codigo = 0;
                Conta.Datavenc = datavenc;
                Conta.Possuiparcela = false;
                DAL.QuitarContasPagarDAL dal = new DAL.QuitarContasPagarDAL();

                dal.QuitarParcelaAnteior(codigo,valorpago);
                dal.GerarNovaParcela(Conta);
                return true;
            }
            else
            return false;
        }

        public bool Estornar(int codigo)
        {


               Models.ContasPagar Conta = new DAL.QuitarContasPagarDAL().ObterDespesa(codigo);
               DAL.QuitarContasPagarDAL dal = new DAL.QuitarContasPagarDAL();
               Models.ContasPagar proxima = new DAL.QuitarContasPagarDAL().ObterProxima(codigo);


               Conta.Valorpago = 0;
               Conta.Pago = false;
                    
            if (proxima == null)
            {
                dal.EstornarUnica(Conta.Codigo);
                return true;
            }
            else
            {

                if (proxima.Pago == true)
                {
                    return false;
                }
                else
                {
                    if (proxima.Pago == false)

                    {
                        dal.EstornarUnica(Conta.Codigo);
                        dal.ApagaProximaParcela(Conta.Codigo);
                        return true;
                    }
                    else
                        return false;

                }

            }

        }

        public bool ExcluirParcela(int codigo)
        {
            Models.ContasPagar Conta = new DAL.QuitarContasPagarDAL().ObterDespesa(codigo);
            DAL.QuitarContasPagarDAL dal = new DAL.QuitarContasPagarDAL();

            dal.EstornarUnica(Conta.Contamae);
            dal.ExcluirParcela(Conta.Codigo);
            return true;
        }
    }
}
