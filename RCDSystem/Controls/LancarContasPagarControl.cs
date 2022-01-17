using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class LancarContasPagarControl
    {
        public bool Gravar(String desc, int tipodes, decimal valor, DateTime datavenc, String detalhes, bool pago)
        {

            Models.TipoDespesa tipodespesa = new DAL.TipoDespesaDAL().ObterDespesa(tipodes);

            Models.ContasPagar contas = new Models.ContasPagar(0,-1,datavenc,desc,detalhes,pago,false,tipodespesa,valor,0);

            DAL.LancarContasPagarDAL dal = new DAL.LancarContasPagarDAL();

            return dal.Gravar(contas);
        }

        public List<Models.ContasPagar> ObterTodos()
        {
            DAL.LancarContasPagarDAL dal = new DAL.LancarContasPagarDAL();

            return dal.ObterTodos();
        }


        public bool Excluir(int cod)
        {
            DAL.QuitarContasPagarDAL dalquitar = new DAL.QuitarContasPagarDAL();
            Models.ContasPagar  despesa = new DAL.QuitarContasPagarDAL().ObterDespesa(cod);

            if (despesa.Pago == false)
            {
                DAL.LancarContasPagarDAL dal = new DAL.LancarContasPagarDAL();
                return dal.Excluir(cod);
            }
            else
                return false;
        }

        public bool Alterar(int codigo, String desc, int tipodes, decimal valor, String detalhe, DateTime datavenc)
        {

            Models.TipoDespesa tipodespesa = new DAL.TipoDespesaDAL().ObterDespesa(tipodes);

            Models.ContasPagar contas = new Models.ContasPagar(codigo, datavenc, desc, detalhe, tipodespesa, valor);

            DAL.LancarContasPagarDAL dal = new DAL.LancarContasPagarDAL();

            return dal.Alterar(contas);
        }
    }
}
