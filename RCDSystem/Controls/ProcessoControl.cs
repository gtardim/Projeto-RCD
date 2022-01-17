using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class ProcessoControl
    {

        public bool Gravar(int cod, String descricao, int cliente, int categoria, decimal valortotal, String observacoes,string numregistro)
        {
            StatusPP status = new DAL.StatusPPDAL().ObterStatus(1);
            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            CategoriaPP cat = new DAL.CategoriaPPDAL().ObterCategoria(categoria);


            Processo processo = new Models.Processo(cod, descricao, cli, cat, status,numregistro ,valortotal, observacoes);

            DAL.ProcessoDAL dal = new DAL.ProcessoDAL();

            return dal.Gravar(processo);
        }

        public bool Excluir(int cod)
        {

            DAL.ProcessoDAL dal = new DAL.ProcessoDAL();
            DAL.ContasReceberDAL dal2 = new DAL.ContasReceberDAL();
            dal2.ExcluirProt(cod, 2);
            return dal.Excluir(cod);

        }
    }
}
