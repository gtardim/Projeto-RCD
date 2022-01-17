using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class TipoArquivoControl
    {

        public bool Gravar(int cod_tipo, string descricao)
        {
            Models.TipoArquivo tpArquivo = new Models.TipoArquivo(cod_tipo, descricao);

            DAL.TipoArquivoDAL dal = new DAL.TipoArquivoDAL();

            return dal.Gravar(tpArquivo);
        }

        public List<Models.TipoArquivo> ObterTodos()
        {
            DAL.TipoArquivoDAL dal = new DAL.TipoArquivoDAL();

            return dal.ObterTodos();
        }

        public Models.TipoArquivo ObterTipo(int cod)
        {
            DAL.TipoArquivoDAL dal = new DAL.TipoArquivoDAL();

            return dal.ObterTipo(cod);
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.TipoArquivoDAL dal = new DAL.TipoArquivoDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int cod_tipo, string descricao)
        {
            Models.TipoArquivo tpArquivo = new Models.TipoArquivo(cod_tipo, descricao);

            DAL.TipoArquivoDAL dal = new DAL.TipoArquivoDAL();

            return dal.Alterar(tpArquivo);
        }
    }
}
