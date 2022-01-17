using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class StatusPPController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int cod_tipo, string descricao)
        {
            bool ok = false;
            Controls.StatusPPControl ctl = new Controls.StatusPPControl();

            ok = ctl.Gravar(cod_tipo, descricao);

            return Json(new { ok });
        }


        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.StatusPPControl ctl = new Controls.StatusPPControl();

            List<Models.StatusPP> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult Excluir(int cod_tipo)
        {
            bool ok = false;
            Controls.StatusPPControl ctl = new Controls.StatusPPControl();

            ok = ctl.Excluir(cod_tipo);

            return Json(new { ok });
        }

        public JsonResult Alterar(int cod_tipo, string descricao)
        {
            bool ok = false;
            Controls.StatusPPControl ctl = new Controls.StatusPPControl();

            ok = ctl.Alterar(cod_tipo, descricao);

            return Json(new { ok });
        }

        public JsonResult VerificaExclusao(int cod)
        {

            return Json(new
            {
                processo = new DAL.ProcessoDAL().VerificaStatusPP(cod),
                protocolo = new DAL.ProtocoloDAL().VerificaStatusPP(cod),
            });
        }
    }
}
