using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class AndamentoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult ObterProcessos()
        {
            return Json(new
            {
                processos = new DAL.ProcessoDAL().ObterTodos()
            });
        }

        public JsonResult ObterProtocolos()
        {

            return Json(new
            {
                protocolos = new DAL.ProtocoloDAL().ObterTodos()
            });

        }


        public JsonResult AlterarProcesso(int codigo, int novostatus)
        {

            bool ok = false;
            Controls.AndamentoControl ctl = new Controls.AndamentoControl();

            ok = ctl.AlterarProcesso(codigo, novostatus);

            return Json(new { ok });
        }

        public JsonResult AlterarProtocolo(int codigo, int novostatus)
        {

            bool ok = false;
            Controls.AndamentoControl ctl = new Controls.AndamentoControl();

            ok = ctl.AlterarProtocolo(codigo, novostatus);

            return Json(new { ok });
        }



        public JsonResult LoadInicial()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                status = new DAL.StatusPPDAL().ObterTodos()
            });
        }
    }
}
