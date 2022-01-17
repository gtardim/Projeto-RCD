using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class LoginControl
    {
        public bool Gravar(int codigo,string usuario,string senha)
        {
            Login login = new Login(codigo, usuario, senha,true);

            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.Gravar(login);
        }

        public bool AtivaUsuario(int codigo)
        {
            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.AtivaUsuario(codigo);
        }
        public bool DesativaUsuario(int codigo)
        {
            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.DesativaUsuario(codigo);
        }

        public List<Login> ObterTodos()
        {
            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.ObterTodos();
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int codigo, string usuario, string senha,bool ativo)
        {
            Login login = new Login(codigo,usuario,senha,ativo);

            DAL.LoginDAL dal = new DAL.LoginDAL();

            return dal.Alterar(login);
        }
    }
}
