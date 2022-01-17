using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class ContasReceberControl
    {
        public bool GerarRecebimentoTotal(int cod, int cliente, decimal valortotal,int tipo)
        {
            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            string desc;

            if (tipo == 1)
                desc ="Protocolo" + ":" + cod;
            else
                desc ="Processo" + ":" + cod;

            ContasReceber contas = new ContasReceber(0,-1,cod, tipo,desc,cli,valortotal,0,Convert.ToDateTime("01/01/0001"),false);

            DAL.ContasReceberDAL dal = new DAL.ContasReceberDAL();

            return dal.Gravar(contas);
        }

        public bool GerarRecebimentoTotalRecebido(int cod, int cliente, decimal valortotal, int tipo)
        {
            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            string desc;

            if (tipo == 1)
                desc = "Protocolo" + ":" + cod;
            else
                desc = "Processo" + ":" + cod;

            ContasReceber contas = new ContasReceber(0, -1, cod, tipo, desc, cli, valortotal, valortotal, Convert.ToDateTime("01/01/0001"), DateTime.Now, false);

            DAL.ContasReceberDAL dal = new DAL.ContasReceberDAL();

            return dal.Gravar(contas);
        }

    }

}

