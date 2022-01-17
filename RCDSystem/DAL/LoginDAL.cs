using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.DAL
{
    public class LoginDAL
    {

        private Banco b;
        internal LoginDAL()
        {
            b = Banco.GetInstance();
        }

        internal List<Models.Login> TableToList(DataTable dt)
        {
            List<Models.Login> dados = new List<Models.Login>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Models.Login usuario = new Models.Login((int)row["codigo"],
                                                                      row["usuario"].ToString(),
                                                                      row["senha"].ToString(),
                                                                      Convert.ToBoolean(row["ativo"]));

                    dados.Add(usuario);
                }
                return dados;

            }
            else
                return null;
        }

        internal Boolean Gravar(Models.Login login)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", login.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@usuario", login.Usuario);
            b.getComandoSQL().Parameters.AddWithValue("@senha", login.Senha);
            b.getComandoSQL().Parameters.AddWithValue("@ativo", login.Ativo);

            b.getComandoSQL().CommandText = @"insert into usuario (codigo,usuario,senha,ativo) 
                                                               values(@codigo,@usuario,@senha,@ativo)";

            return b.ExecutaComando() == 1;
        }

        internal List<Models.Login> ObterTodos()
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select codigo,usuario,senha,ativo
                                              from usuario";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal List<Models.Login> ValidaUsuario(string usuario)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@usuario", usuario);

            b.getComandoSQL().CommandText = @"select codigo,usuario,senha,ativo
                                              from usuario
                                              where usuario = @usuario";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal bool ValidaUsuarioSenha(string usuario, string senha)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@usuario", usuario);
            b.getComandoSQL().Parameters.AddWithValue("@senha", senha);

            b.getComandoSQL().CommandText = @"select codigo,usuario,senha,ativo
                                              from usuario
                                              where usuario = @usuario and
                                              senha = @senha";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal bool VerificaAtivo(string usuario)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@usuario", usuario);

            b.getComandoSQL().CommandText = @"select codigo,usuario,senha,ativo
                                              from usuario
                                              where usuario = @usuario and
                                              ativo = 1";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal Models.Login ObterUsuario(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);


            b.getComandoSQL().CommandText = @"select codigo, usuario,senha,ativo
                                              from usuario
                                              where codigo = @codigo";

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Models.Login usuario = new Models.Login((int)row["codigo"],
                                                                    row["usuario"].ToString(),
                                                                    row["senha"].ToString(),
                                                                    Convert.ToBoolean(row["ativo"]));

                return usuario;
            }

            else
                return null;
        }

        internal bool Excluir(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);

            b.getComandoSQL().CommandText = @"delete from usuario
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool Alterar(Models.Login login)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", login.Codigo);
            b.getComandoSQL().Parameters.AddWithValue("@usuario", login.Usuario);
            b.getComandoSQL().Parameters.AddWithValue("@senha", login.Senha);
            b.getComandoSQL().Parameters.AddWithValue("@ativo", login.Ativo);

            b.getComandoSQL().CommandText = @"update usuario
                                              set usuario  = @usuario,
                                              ativo  = @ativo
                                              senha  = @senha
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
        internal bool AtivaUsuario(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);


            b.getComandoSQL().CommandText = @"update usuario
                                              set ativo  = 1
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }

        internal bool DesativaUsuario(int codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().Parameters.AddWithValue("@codigo", codigo);


            b.getComandoSQL().CommandText = @"update usuario
                                              set ativo  = 0
                                              where codigo = @codigo";

            return b.ExecutaComando() == 1;
        }
    }
}
