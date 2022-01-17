using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class TipoClienteDAL
    {

        private Banco b;
        internal TipoClienteDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.TipoCliente> TableToList(DataTable dt)
        {
            List<Models.TipoCliente> dados = null;
            dados = (from DataRow row in dt.Rows
                     select new Models.TipoCliente((int)row["codigo"],
                                                    row["descricao"].ToString())).ToList();


            return dados;
        }

        internal Boolean Gravar(Models.TipoCliente tpCliente)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpCliente.Descricao);

            b.getComandoSQL().CommandText = @"insert into tipocliente (descricao) 
                                                               values(@descricao)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.TipoCliente> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo, descricao
                                              from tipocliente";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool Excluir(int cod_tipo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", cod_tipo);

            b.getComandoSQL().CommandText = @"delete from tipocliente
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.TipoCliente tpCliente)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", tpCliente.Cod_tipo);
            b.getComandoSQL().Parameters.AddWithValue("@descricao", tpCliente.Descricao);

            b.getComandoSQL().CommandText = @"update tipocliente
                                              set descricao = @descricao
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

    }
}
