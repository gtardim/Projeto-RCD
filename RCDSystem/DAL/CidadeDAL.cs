using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class CidadeDAL
    {
        private Banco b;
        internal CidadeDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Cidade> TableToList(DataTable dt)
        {
            List<Models.Cidade> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.Cidade((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal List<Models.Cidade> ObterCidades(int id)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo",id);

            b.getComandoSQL().CommandText = @"SELECT codigo,descricao 
                                                FROM cidade 
                                            where estado = @codigo;";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.Cidade ObterCidade(int id)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", id);

            b.getComandoSQL().CommandText = @"SELECT codigo,descricao,estado 
                                                FROM cidade 
                                            where codigo = @codigo;";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            { 
                DataRow row = dt.Rows[0];
                Models.Cidade cidade = new Models.Cidade((int)row["codigo"], row["descricao"].ToString(), (int)row["estado"]);
                return cidade;
            }
            else
                return null;
        }
    }
}
