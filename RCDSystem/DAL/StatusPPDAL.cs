using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class StatusPPDAL
    {

         private Banco b;
        internal StatusPPDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.StatusPP> TableToList(DataTable dt)
        {
            List<Models.StatusPP> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.StatusPP((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal Boolean Gravar(Models.StatusPP statusPP)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@descricao", statusPP.Descricao);

            b.getComandoSQL().CommandText = @"insert into statusPP (descricao) 
                                                               values(@descricao)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.StatusPP> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from statuspp";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.StatusPP ObterStatus(int status)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", status);

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from statuspp
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.StatusPP statuspp = new Models.StatusPP((int)row["codigo"], row["descricao"].ToString());

                return statuspp;
            }
            else
                return null;
        }

        internal bool Excluir(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"delete from statuspp
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.StatusPP statusPP)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", statusPP.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", statusPP.Descricao);

            b.getComandoSQL().CommandText = @"update statuspp
                                              set descricao = @descricao
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
