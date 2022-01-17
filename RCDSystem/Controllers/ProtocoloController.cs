using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class ProtocoloController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioProtocolos()
        {
            return View();
        }
        public JsonResult Gravar(int cod,string descricao,int cliente,int categoria,decimal valortotal,string observacoes)
        {

            bool ok = false;

            Controls.ProtocoloControl ctl = new Controls.ProtocoloControl();
            ok = ctl.Gravar(cod,descricao, cliente, categoria, valortotal, observacoes);

            return Json(new { ok,
                              codigo = new DAL.ProtocoloDAL().ObterCodNovo(descricao)
            });
        }



        public JsonResult Excluir(int cod)
        {
            bool ok = false;
            bool atendimento = false;


            if (new DAL.AtendimentoDAL().VerificaAtendimentos(cod) == false)
            {
                Controls.ProtocoloControl ctl = new Controls.ProtocoloControl();
                ok = ctl.Excluir(cod);
                atendimento = true;
            }
           
            

            

            return Json(new { ok ,
                              atendimento  });
        }


        public JsonResult LoadInicial()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                categoria = new DAL.CategoriaPPDAL().ObterTodos()
            });
        }

        public JsonResult LoadRelatorio()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                status = new DAL.StatusPPDAL().ObterTodos()
            });
        }

        public JsonResult LoadBusca()
        {

            return Json(new
            {
                protocolo = new DAL.ProtocoloDAL().ObterTodos()
            });
        }

        public JsonResult LoadAtendimento(int cod)
        {

            return Json(new
            {
                atendimento = new DAL.AtendimentoDAL().ObterAtendimentos(cod)
            }); ;
        }

        public JsonResult ValidaDescricaoProtocolo(string descricao)
        {

            return Json(new
            {
                ok = new DAL.ProtocoloDAL().ValidaDescricao(descricao)
            }); ; ;
        }

        public JsonResult ValidaAtendimento(int codprotocolo,DateTime diaatend, DateTime horainicio, DateTime horafim)
        {

            return Json(new
            {
                ok = new DAL.AtendimentoDAL().ValidaAtendimento(codprotocolo, diaatend, horainicio, horafim)
            }); ; ;
        }

        public JsonResult BuscaCod(string descricao)
        {

            return Json(new
            {
                cod = new DAL.ProtocoloDAL().ObterCodNovo(descricao)
            }); ;
        }

        private Reports.ProtocoloReports getRelatorio(int tipodes,int status, string data_i, string data_f)
        {
            var rpt = new Reports.ProtocoloReports(tipodes, status, data_i, data_f);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Protocolos";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int cliente,int status, string data_i, string data_f)
        {
            var rpt = getRelatorio(cliente,status, data_i, data_f);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

    }
}
