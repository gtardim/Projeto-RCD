using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class EstadoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.EstadoControl ctl = new Controls.EstadoControl();

            List<Models.Estado> dados = ctl.ObterTodos();

            return Json(new { dados });
        }
    }
}
