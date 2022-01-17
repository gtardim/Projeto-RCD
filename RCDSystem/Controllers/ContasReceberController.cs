using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RCDSystem.Controllers
{
    public class ContasReceberController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GerarRecebimentoTotal(int cod, int cliente, decimal valortotal, int tipo)
        {

            decimal valort = Convert.ToDecimal(valortotal.ToString().Replace('.', ','));
            bool ok = false;
            Controls.ContasReceberControl ctl = new Controls.ContasReceberControl();

            ok = ctl.GerarRecebimentoTotal(cod,cliente,valort,tipo);

            return Json(new { ok });
        }

        public JsonResult GerarRecebimentoTotalRecebido(int cod, int cliente, decimal valortotal, int tipo)
        {

            decimal valort = Convert.ToDecimal(valortotal.ToString().Replace('.', ','));
            bool ok = false;
            Controls.ContasReceberControl ctl = new Controls.ContasReceberControl();

            ok = ctl.GerarRecebimentoTotalRecebido(cod, cliente, valort, tipo);

            return Json(new { ok });
        }

        public JsonResult AtualizaRecebimento(decimal valortotal, int cod, int tipo)
        {

            decimal valort = Convert.ToDecimal(valortotal.ToString().Replace('.', ','));
            bool ok = false;

            if(new DAL.ContasReceberDAL().ObterValorRecebido(cod, tipo) == 0)
            {
                DAL.ContasReceberDAL dal = new DAL.ContasReceberDAL();
                ok = dal.AtualizaRecebimento(cod, tipo, valort);
            }

            return Json(new { ok });
        }


        public JsonResult ValorRecebido(int cod, int tipo)
        {

            return Json(new
            {
                valorrecebido = new DAL.ContasReceberDAL().ObterValorRecebido(cod,tipo)
            }) ;
        }
    }
}