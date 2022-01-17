using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class QuitarContasReceberControl
    {

        public bool ReceberTotal(int codigo,int tipo )
        {
            Models.ContasReceber recebimento = new DAL.ContasReceberDAL().ObterRecebimento(codigo,tipo);

            DAL.QuitarContasReceberDAL dal = new DAL.QuitarContasReceberDAL();

            return dal.ReceberTotal(recebimento);
        }

        public bool RecebimentoParcial(int codigo, decimal valorpago, DateTime datavenc,int tipo)
        {
            if (valorpago > 0)
            {
                Models.ContasReceber Conta = new DAL.ContasReceberDAL().ObterRecebimento(codigo,tipo);
                Conta.Valor = Conta.Valor - valorpago;
                Conta.Valorpago = 0;
                Conta.Contamae = Conta.Codigo;
                Conta.Codigo = 0;
                Conta.Datageracao = datavenc;
                Conta.Possuiparcela = false;
                DAL.QuitarContasReceberDAL dal = new DAL.QuitarContasReceberDAL();

                dal.QuitarRecebimentoAnteior(codigo, valorpago,Conta.Tipo);
                dal.GerarNovaParcela(Conta);
                return true;
            }
            else
                return false;
        }

        public bool Estornar(int codigo, int tipo)
        {


            Models.ContasReceber Conta = new DAL.ContasReceberDAL().ObterRecebimento(codigo, tipo);
            DAL.QuitarContasReceberDAL dal = new DAL.QuitarContasReceberDAL();

            Models.ContasReceber proxima = new DAL.QuitarContasReceberDAL().ObterProxima(codigo, tipo);

            Conta.Valorpago = 0;
            Conta.Pago = null;

            if (proxima == null)
            {
                dal.EstornarUnica(Conta.Codigo, Conta.Tipo);
                return true;
            }
            else
            {

                if (proxima.Pago != null)
                {
                    return false;
                }
                else
                {
                    if (proxima.Pago == null)

                    {
                        dal.EstornarUnica(Conta.Codigo, Conta.Tipo);
                        dal.ApagaProximaParcela(Conta.Codigo, Conta.Tipo);
                        return true;
                    }
                    else
                        return false;

                }

            }

        }
    }
}
