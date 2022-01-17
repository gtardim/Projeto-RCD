using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class TipoArquivoDAL
    {

        private Banco b;
        internal TipoArquivoDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.TipoArquivo> TableToList(DataTable dt)
        {
           
            List<Models.TipoArquivo> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.TipoArquivo((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal Boolean Gravar(Models.TipoArquivo tpArquivo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpArquivo.Descricao);

            b.getComandoSQL().CommandText = @"insert into tipoarquivo (descricao) 
                                                               values(@descricao)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.TipoArquivo> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from tipoarquivo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.TipoArquivo ObterTipo(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from tipoarquivo
                                            where codigo = @codigo;";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.TipoArquivo tipoarq = new Models.TipoArquivo((int)row["codigo"], row["descricao"].ToString());
                return tipoarq;
            }
            else
                return null;
        }

        internal bool Excluir(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"delete from tipoarquivo
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.TipoArquivo tpArquivo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", tpArquivo.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpArquivo.Descricao);

            b.getComandoSQL().CommandText = @"update tipoarquivo
                                              set descricao = @descricao
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
