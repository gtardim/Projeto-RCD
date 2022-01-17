using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class CategoriaPPController : Controller
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
            Controls.CategoriaPPControl ctl = new Controls.CategoriaPPControl();

            ok = ctl.Gravar(cod_tipo, descricao);

            return Json(new { ok });
        }


        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.CategoriaPPControl ctl = new Controls.CategoriaPPControl();

            List<Models.CategoriaPP> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult ValidaExclusao(int cod_tipo)
        {
            return Json(new
            {
                processo = new DAL.ProcessoDAL().ValidaCategoriaPP(cod_tipo),
                protocolo = new DAL.ProtocoloDAL().ValidaCategoriaPP(cod_tipo)
            });
        }

        public JsonResult Excluir(int cod_tipo)
        {
            bool ok = false;
            Controls.CategoriaPPControl ctl = new Controls.CategoriaPPControl();

            ok = ctl.Excluir(cod_tipo);

            return Json(new { ok });
        }

        public JsonResult Alterar(int cod_tipo, string descricao)
        {
            bool ok = false;
            Controls.CategoriaPPControl ctl = new Controls.CategoriaPPControl();

            ok = ctl.Alterar(cod_tipo, descricao);

            return Json(new { ok });
        }
    }
}
