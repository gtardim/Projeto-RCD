using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class EstadoDAL
    {

        private Banco b;
        internal EstadoDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Estado> TableToList(DataTable dt)
        {
            List<Models.Estado> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.Estado((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal List<Models.Estado> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from estado";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

    }
}
