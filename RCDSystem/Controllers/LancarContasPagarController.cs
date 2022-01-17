using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RCDSystem.Controllers
{
    public class LancarContasPagarController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Gravar(String desc,int tipodes,decimal valor,DateTime datavenc,String detalhe,bool pago)
        {

            bool ok = false;
            int cliente = Convert.ToInt32(Request.Form["cliente"]);
            int tipoarq = Convert.ToInt32(Request.Form["tipoarq"]);
            String descricao = Request.Form["descricao"];

              Controls.LancarContasPagarControl ctl = new Controls.LancarContasPagarControl();
              ok = ctl.Gravar(desc,tipodes, valor, datavenc, detalhe,pago);

            return Json(new { ok });
        }

        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.LancarContasPagarControl ctl = new Controls.LancarContasPagarControl();

            List<Models.ContasPagar> dados = ctl.ObterTodos();

            return Json(new { dados });
        }


        public JsonResult LoadInicial()
        {
           
            return Json(new{tipodespesa = new DAL.TipoDespesaDAL().ObterTodos(),
                despesas = new DAL.LancarContasPagarDAL().ObterTodos() });
        }

        public JsonResult LoadReport()
        {

            return Json(new
            {
                tipodespesa = new DAL.TipoDespesaDAL().ObterTodos()
            });
        }

        public JsonResult Alterar(int codigo,String desc,int tipodes,decimal valor,String detalhe, DateTime datavenc)
        {
            bool ok = false;
            Controls.LancarContasPagarControl ctl = new Controls.LancarContasPagarControl();

            ok = ctl.Alterar(codigo, desc, tipodes, valor, detalhe, datavenc);

            return Json(new { ok });
        }
        public JsonResult Excluir(int cod)
        {
            bool ok = false;
            Controls.LancarContasPagarControl ctl = new Controls.LancarContasPagarControl();

            ok = ctl.Excluir(cod);

            return Json(new { ok });
        }

        public JsonResult ValidaPagamento(int codigo)
        {
            Models.ContasPagar despesa = new DAL.QuitarContasPagarDAL().ObterDespesa(codigo);

            if (despesa.Pago == true)
            {
                return Json(new
                {
                    msg = "Essa Conta Ja Possui Pagamento"

                });
            }
            else
            {
                return Json(new
                {
                    msg = ""

                });
            }
        }

    }
}