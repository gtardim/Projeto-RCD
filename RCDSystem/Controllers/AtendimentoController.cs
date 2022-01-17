using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class AtendimentoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioAtendimentos()
        {
            return View();
        }

        public JsonResult LoadInicial()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                protocolo = new DAL.ProtocoloDAL().ObterProtocolosFiltroRP()
            });
        }

        public JsonResult GravarAtendimento(int cod, String titulo, String atendimento, DateTime diaatend, DateTime horainicio, DateTime horafim, int codprotocolo)
        {

            bool ok = false;

            Controls.ProtocoloControl ctl = new Controls.ProtocoloControl();
            ok = ctl.GravarAtendimento(cod, titulo, atendimento, diaatend, horainicio, horafim, codprotocolo);

            return Json(new { ok });
        }

        public JsonResult AlterarAtendimento(int cod, String titulo, String atendimento, DateTime diaatend, DateTime horainicio, DateTime horafim, int codprotocolo)
        {

            bool ok = false;

            Controls.AtendimentoControl ctl = new Controls.AtendimentoControl();
            ok = ctl.AlterarAtendimento(cod, titulo, atendimento, diaatend, horainicio, horafim, codprotocolo);

            return Json(new { ok });
        }

        public JsonResult Excluir(int cod)
        {
            bool ok = false;
            Controls.AtendimentoControl ctl = new Controls.AtendimentoControl();

            ok = ctl.Excluir(cod);

            return Json(new { ok });
        }

        private Reports.AtendimentoReports getRelatorio(int cliente, string data_i, string data_f, int protocolo)
        {
            var rpt = new Reports.AtendimentoReports(cliente, data_i, data_f,protocolo);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Atendimento";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int cliente, string data_i, string data_f, int protocolo)
        {
            var rpt = getRelatorio(cliente, data_i, data_f, protocolo);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }
    }
}
