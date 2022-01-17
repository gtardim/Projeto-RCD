using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class QuitarContasPagarController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioContasPagar()
        {
            return View();
        }

        public JsonResult ObterAbertas(DateTime dataini,DateTime datafim)
        {
            DateTime teste = new DateTime(0001, 1, 1, 0, 0, 0);

            if (dataini != teste && datafim == teste)
            {

                return Json(new
                {
                    despesas = new DAL.QuitarContasPagarDAL().ObterAbertas1Data(dataini)
                });
            }
            else
                if(dataini != teste && datafim != teste)
                {
                    return Json(new
                    {
                        despesas = new DAL.QuitarContasPagarDAL().ObterAbertas2Data(dataini,datafim)
                    });
                 }
                 else
                {
                    return Json(new
                    {
                        despesas = new DAL.QuitarContasPagarDAL().ObterAbertas()
                    });
                }
          
        }

        public JsonResult ObterPagas(DateTime dataini, DateTime datafim)
        {

            DateTime teste = new DateTime(0001, 1, 1, 0, 0, 0);

            if (dataini != teste && datafim == teste)
            {

                return Json(new
                {
                    despesas = new DAL.QuitarContasPagarDAL().ObterPagas1Data(dataini)
                });
            }
            else
                if (dataini != teste && datafim != teste)
            {
                return Json(new
                {
                    despesas = new DAL.QuitarContasPagarDAL().ObterPagas2Data(dataini, datafim)
                });
            }
            else
            {
                return Json(new
                {
                    despesas = new DAL.QuitarContasPagarDAL().ObterPagas()
                });
            }



        }
        public JsonResult PagarTotal(int codigo)
        {

            bool ok = false;
            Controls.QuitarContasPàgarControl ctl = new Controls.QuitarContasPàgarControl();

            ok = ctl.PagarTotal(codigo);

            return Json(new { ok });
        }

        public JsonResult PagarParcial(int codigo,decimal valorpago,DateTime datavenc)
        {

            bool ok = false;
            Controls.QuitarContasPàgarControl ctl = new Controls.QuitarContasPàgarControl();

            ok = ctl.PagarParcial(codigo,valorpago,datavenc);

            return Json(new { ok });
        }

        public JsonResult Estornar(int codigo)
        {

            bool ok = false;
            Controls.QuitarContasPàgarControl ctl = new Controls.QuitarContasPàgarControl();

            ok = ctl.Estornar(codigo);

            return Json(new { ok });
        }

        public JsonResult ExcluirParcela(int codigo)
        {
            bool ok = false;
            Controls.QuitarContasPàgarControl ctl = new Controls.QuitarContasPàgarControl();

            ok = ctl.ExcluirParcela(codigo);

            return Json(new { ok });
        }

        private Reports.ContasPagarReports getRelatorio(int tipodes, string data_i, string data_f, bool chkPagas )
        {
            var rpt = new Reports.ContasPagarReports(tipodes, data_i, data_f, chkPagas);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            if(chkPagas)
                rpt.PageTitle = "Relatório de Contas Pagas";

            else
                rpt.PageTitle = "Relatório de Contas à Pagar";

            
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int tipodes, string data_i, string data_f,bool chkPagas)
        {
            var rpt = getRelatorio(tipodes, data_i, data_f, chkPagas);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

        private Reports.ContasVencidasReports getRelatorioVencidas()
        {
            var rpt = new Reports.ContasVencidasReports();
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Contas Vencidas";

            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult PreviewVencidas()
        {
            var rpt = getRelatorioVencidas();

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }
    }
}
