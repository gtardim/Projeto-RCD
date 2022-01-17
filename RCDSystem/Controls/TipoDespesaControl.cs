using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class TipoDespesaControl
    {

        public bool Gravar(int cod_tipo, string descricao)
        {
            Models.TipoDespesa tpArquivo = new Models.TipoDespesa(cod_tipo, descricao);

            DAL.TipoDespesaDAL dal = new DAL.TipoDespesaDAL();

            return dal.Gravar(tpArquivo);
        }

        public List<Models.TipoDespesa> ObterTodos()
        {
            DAL.TipoDespesaDAL dal = new DAL.TipoDespesaDAL();

            return dal.ObterTodos();
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.TipoDespesaDAL dal = new DAL.TipoDespesaDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int cod_tipo, string descricao)
        {
            Models.TipoDespesa tpArquivo = new Models.TipoDespesa(cod_tipo, descricao);

            DAL.TipoDespesaDAL dal = new DAL.TipoDespesaDAL();

            return dal.Alterar(tpArquivo);
        }
    }
}
