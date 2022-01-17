using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class CategoriaPPDAL
    {


        private Banco b;
        internal CategoriaPPDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.CategoriaPP> TableToList(DataTable dt)
        {
            List<Models.CategoriaPP> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.CategoriaPP((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal Boolean Gravar(Models.CategoriaPP catPP)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@descricao", catPP.Descricao);

            b.getComandoSQL().CommandText = @"insert into categoriapp (descricao) 
                                                               values(@descricao)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.CategoriaPP> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from categoriapp";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.CategoriaPP ObterCategoria(int categoria)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", categoria);
            

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from categoriapp
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.CategoriaPP cat = new Models.CategoriaPP((int)row["codigo"], row["descricao"].ToString()); 

                return cat;
            }
                
            else
                return null;
        }

        internal bool Excluir(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"delete from categoriapp
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.CategoriaPP catPP)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", catPP.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", catPP.Descricao);

            b.getComandoSQL().CommandText = @"update categoriapp
                                              set descricao = @descricao
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
