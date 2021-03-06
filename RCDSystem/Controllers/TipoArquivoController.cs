using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class TipoArquivoController : Controller
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
            Controls.TipoArquivoControl ctl = new Controls.TipoArquivoControl();

            ok = ctl.Gravar(cod_tipo, descricao);

            return Json(new { ok });
        }


        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.TipoArquivoControl ctl = new Controls.TipoArquivoControl();

            List<Models.TipoArquivo> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult ObterTipo(int cod)
        {
            bool ok = false;
            Controls.TipoArquivoControl ctl = new Controls.TipoArquivoControl();

            Models.TipoArquivo dados = ctl.ObterTipo(cod);

            return Json(new { dados });
        }
        public JsonResult Excluir(int cod_tipo)
        {
            bool ok = false;
            Controls.TipoArquivoControl ctl = new Controls.TipoArquivoControl();

            ok = ctl.Excluir(cod_tipo);

            return Json(new { ok });
        }

        public JsonResult Alterar(int cod_tipo, string descricao)
        {
            bool ok = false;
            Controls.TipoArquivoControl ctl = new Controls.TipoArquivoControl();

            ok = ctl.Alterar(cod_tipo, descricao);

            return Json(new { ok });
        }


        public JsonResult ValidaExclusao(int cod_tipo)
        {

            return Json(new
            {
                tipoarq = new DAL.RegistrarArquivoDAL().VerificaTipoArquivo(cod_tipo)
            }) ;
        }
    }
}
