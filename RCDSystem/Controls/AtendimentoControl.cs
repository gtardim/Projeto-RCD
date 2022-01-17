using RCDSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controls
{
    public class AtendimentoControl
    {
        public bool Excluir(int cod)
        {

            DAL.AtendimentoDAL dal = new DAL.AtendimentoDAL();

            return dal.Excluir(cod);
        }

        public bool AlterarAtendimento(int cod, String titulo, String atendimento, DateTime diaatend, DateTime horainicio, DateTime horafim, int codprotocolo)
        {

            Protocolo prot = new DAL.ProtocoloDAL().ObterProtocolo(codprotocolo);
            Atendimento Atend = new Models.Atendimento(cod, titulo, prot, 0, diaatend, horainicio, horafim, atendimento);

            DAL.AtendimentoDAL dal = new DAL.AtendimentoDAL();

            return dal.AlterarAtendimento(Atend);
        }
    }
}
