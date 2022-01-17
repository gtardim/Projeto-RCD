using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class TipoClienteControl
    {



        public bool Gravar(int cod_tipo, string descricao)
        {
            Models.TipoCliente tpCliente = new Models.TipoCliente(cod_tipo, descricao);

            DAL.TipoClienteDAL dal = new DAL.TipoClienteDAL();

            return dal.Gravar(tpCliente);
        }

        public List<Models.TipoCliente> ObterTodos()
        {
            DAL.TipoClienteDAL dal = new DAL.TipoClienteDAL();

            return dal.ObterTodos();
        }

        public bool Excluir(int cod_tipo)
        {

            DAL.TipoClienteDAL dal = new DAL.TipoClienteDAL();

            return dal.Excluir(cod_tipo);
        }

        public bool Alterar(int cod_tipo, string descricao)
        {
            Models.TipoCliente tpCliente = new Models.TipoCliente(cod_tipo, descricao);

            DAL.TipoClienteDAL dal = new DAL.TipoClienteDAL();

            return dal.Alterar(tpCliente);
        }
    }
}
