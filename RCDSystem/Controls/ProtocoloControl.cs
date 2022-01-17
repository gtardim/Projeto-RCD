using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class ProtocoloControl
    {

        public bool Gravar(int cod,String descricao, int cliente, int categoria, decimal valortotal, String observacoes)
        {
            StatusPP status = new DAL.StatusPPDAL().ObterStatus(1);
            Cliente cli = new DAL.ClienteDAL().ObterCliente(cliente);
            CategoriaPP cat = new DAL.CategoriaPPDAL().ObterCategoria(categoria);
            

            Protocolo protocolo = new Models.Protocolo(cod,descricao,cli,cat,status,valortotal,-1,observacoes);

            DAL.ProtocoloDAL dal = new DAL.ProtocoloDAL();

            return dal.Gravar(protocolo);
        }

        public bool GravarAtendimento(int cod, String titulo, String atendimento, DateTime diaatend, DateTime horainicio, DateTime horafim, int codprotocolo)
        {

            Protocolo prot = new DAL.ProtocoloDAL().ObterProtocolo(codprotocolo);
            Atendimento Atend = new Models.Atendimento(cod,titulo,prot,0,diaatend,horainicio,horafim,atendimento);

            DAL.AtendimentoDAL dal = new DAL.AtendimentoDAL();

            return dal.GravarAtendimento(Atend);
        }



        public bool Excluir(int cod)
        {

                DAL.ProtocoloDAL dal = new DAL.ProtocoloDAL();
                DAL.ContasReceberDAL dal2 = new DAL.ContasReceberDAL();
                dal2.ExcluirProt(cod, 1);
                return dal.Excluir(cod);

        }
    }
}
