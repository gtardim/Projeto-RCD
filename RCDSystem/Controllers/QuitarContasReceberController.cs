using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class QuitarContasReceberController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioContasReceber()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioRecebidos()
        {
            return View();
        }

        public JsonResult ObterAbertas(DateTime dataini, DateTime datafim)
        {
            DateTime teste = new DateTime(0001, 1, 1, 0, 0, 0);

            if (dataini != teste && datafim == teste)
            {

                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterAbertas1Data(dataini)
                });
            }
            else
                if (dataini != teste && datafim != teste)
            {
                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterAbertas2Data(dataini, datafim)
                });
            }
            else
            {
                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterAbertas()
                });
            }

        }

        public JsonResult ObterBaixadas(DateTime dataini, DateTime datafim)
        {

            DateTime teste = new DateTime(0001, 1, 1, 0, 0, 0);

            if (dataini != teste && datafim == teste)
            {

                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterPagas1Data(dataini)
                });
            }
            else
                if (dataini != teste && datafim != teste)
            {
                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterPagas2Data(dataini, datafim)
                });
            }
            else
            {
                return Json(new
                {
                    recebimentos = new DAL.QuitarContasReceberDAL().ObterPagas()
                });
            }



        }
        public JsonResult ReceberTotal(int codigo,int tipo)
        {

            bool ok = false;
            Controls.QuitarContasReceberControl ctl = new Controls.QuitarContasReceberControl();

            ok = ctl.ReceberTotal(codigo,tipo);

            return Json(new { ok });
        }

        public JsonResult RecebimentoParcial(int codigo, decimal valorpago, DateTime datareceb,int tipo)
        {

            bool ok = false;
            Controls.QuitarContasReceberControl ctl = new Controls.QuitarContasReceberControl();

            ok = ctl.RecebimentoParcial(codigo, valorpago, datareceb,tipo);

            return Json(new { ok });
        }

        public JsonResult Estornar(int codigo, int tipo)
        {

            bool ok = false;
            Controls.QuitarContasReceberControl ctl = new Controls.QuitarContasReceberControl();

            ok = ctl.Estornar(codigo,tipo);

            return Json(new { ok });
        }

        public JsonResult ExcluirParcela(int codigo)
        {
            bool ok = false;
            Controls.QuitarContasPàgarControl ctl = new Controls.QuitarContasPàgarControl();

            ok = ctl.ExcluirParcela(codigo);

            return Json(new { ok });
        }

        public JsonResult LoadInicial()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos()
            });
        }

        private Reports.ContasReceberReports getRelatorio(int cliente, string data_i, string data_f)
        {
            var rpt = new Reports.ContasReceberReports(cliente,data_i, data_f);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Contas a Receber";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int cliente, string data_i, string data_f)
        {
            var rpt = getRelatorio(cliente, data_i, data_f);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

        private Reports.ContasRecebidasReports getRelatorio2(int cliente, string data_i, string data_f)
        {
            var rpt = new Reports.ContasRecebidasReports(cliente, data_i, data_f);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Contas a Recebidas";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview2(int cliente, string data_i, string data_f)
        {
            var rpt = getRelatorio2(cliente, data_i, data_f);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

        private Reports.ReciboReports getRecibo(int codigo)
        {
            var rpt = new Reports.ReciboReports(codigo);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Recibo de Pagamento";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Recibo(int codigo)
        {
            var rpt = getRecibo(codigo);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }
    }
}
