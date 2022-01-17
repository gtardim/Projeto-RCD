using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class StatusPPControl
    {

        public bool Gravar(int cod_tipo, string descricao)
        {
            Models.StatusPP statusPP = new Models.StatusPP(cod_tipo, descricao);

            DAL.StatusPPDAL dal = new DAL.StatusPPDAL();

            return dal.Gravar(statusPP);
        }

        public List<Models.StatusPP> ObterTodos()
        {
            DAL.StatusPPDAL dal = new DAL.StatusPPDAL();

            return dal.ObterTodos();
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.StatusPPDAL dal = new DAL.StatusPPDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int cod_tipo, string descricao)
        {
            Models.StatusPP statusPP = new Models.StatusPP(cod_tipo, descricao);

            DAL.StatusPPDAL dal = new DAL.StatusPPDAL();

            return dal.Alterar(statusPP);
        }
    }
}
