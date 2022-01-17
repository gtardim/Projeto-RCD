using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RCDSystem.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class TipoClienteController : Controller
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
                Controls.TipoClienteControl ctl = new Controls.TipoClienteControl();

                ok = ctl.Gravar(cod_tipo, descricao);

                return Json(new { ok });
            }


            public JsonResult ObterTodos()
            {
                bool ok = false;
                Controls.TipoClienteControl ctl = new Controls.TipoClienteControl();

                List<Models.TipoCliente> dados = ctl.ObterTodos();

                return Json(new { dados });
            }

        public JsonResult Excluir(int cod_tipo)
        {
            bool ok = false;
            Controls.TipoClienteControl ctl = new Controls.TipoClienteControl();

            ok = ctl.Excluir(cod_tipo);

            return Json(new { ok,Error.ErroSql});
        }

        public JsonResult Alterar(int cod_tipo, string descricao)
        {
            bool ok = false;
            Controls.TipoClienteControl ctl = new Controls.TipoClienteControl();

            ok = ctl.Alterar(cod_tipo, descricao);

            return Json(new { ok });
        }


    }
}