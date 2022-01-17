using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class TipoDespesaDAL
    {

        private Banco b;
        internal TipoDespesaDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.TipoDespesa> TableToList(DataTable dt)
        {
            List<Models.TipoDespesa> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.TipoDespesa((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal Boolean Gravar(Models.TipoDespesa tpDespesa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpDespesa.Descricao);

            b.getComandoSQL().CommandText = @"insert into tipodespesa (descricao) 
                                                               values(@descricao)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.TipoDespesa> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from tipodespesa";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Models.TipoDespesa ObterDespesa(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from tipodespesa
                                               where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                Models.TipoDespesa tipoDespesa = new Models.TipoDespesa((int)row["codigo"], row["descricao"].ToString());

                return tipoDespesa;
            }
                
            else
                return null;
        }

        internal bool Excluir(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"delete from tipodespesa
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.TipoDespesa tpDespesa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", tpDespesa.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpDespesa.Descricao);

            b.getComandoSQL().CommandText = @"update tipodespesa
                                              set descricao = @descricao
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
