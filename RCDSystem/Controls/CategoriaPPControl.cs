using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class CategoriaPPControl
    {

        public bool Gravar(int cod_tipo, string descricao)
        {
            CategoriaPP catPP = new CategoriaPP(cod_tipo, descricao);

            DAL.CategoriaPPDAL dal = new DAL.CategoriaPPDAL();

            return dal.Gravar(catPP);
        }

        public List<CategoriaPP> ObterTodos()
        {
            DAL.CategoriaPPDAL dal = new DAL.CategoriaPPDAL();

            return dal.ObterTodos();
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.CategoriaPPDAL dal = new DAL.CategoriaPPDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int cod_tipo, string descricao)
        {
            CategoriaPP catPP = new CategoriaPP(cod_tipo, descricao);

            DAL.CategoriaPPDAL dal = new DAL.CategoriaPPDAL();

            return dal.Alterar(catPP);
        }
    }
}
