using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RCDSystem.Controllers
{
    public class ClienteController : Controller
    {
        // GET: /<controller>/
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Gravar(string nome, string nacionalidade, string estadocivil, string RG, string CPFCNPJ, string rua, string bairro, string numero, int cidade, string CEP, string email, string contato, int representante, bool chekCPF)
        {
            bool ok = false;


            Controls.ClienteControl ctl = new Controls.ClienteControl();
            ok = ctl.Gravar(nome, nacionalidade, estadocivil, RG, CPFCNPJ, rua, bairro, numero, cidade, CEP, email, contato, representante, chekCPF);

            return Json(new { ok });


        }

        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.ClienteControl ctl = new Controls.ClienteControl();

            List<Models.Cliente> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult ObterCliente(int cod)
        {
            bool ok = false;
            Controls.ClienteControl ctl = new Controls.ClienteControl();

            Models.Cliente dados = ctl.ObterCliente(cod);

            return Json(new { dados });
        }


        public JsonResult Excluir(int cod)
        {
            bool ok = false;
            Controls.ClienteControl ctl = new Controls.ClienteControl();

            ok = ctl.Excluir(cod);

            return Json(new { ok });
        }

        public JsonResult VerificaExclusao(int cod)
        {

            return Json(new { 
                arq = new DAL.RegistrarArquivoDAL().VerificaClienteArquivo(cod),
                processo = new DAL.ProcessoDAL().VerificaProcessoCliente(cod),
                protocolo = new DAL.ProtocoloDAL().VerificaProtocoloCliente(cod),
            });
        }

        public JsonResult VerificaCPFexistente(string CPFCNPJ)
        {
       
            return Json(new { cpf = new DAL.ClienteDAL().verificaCPFexistente(CPFCNPJ) });
        }

        public JsonResult VerificaCNPJexistente(string CPFCNPJ)
        {

            return Json(new { cnpj = new DAL.ClienteDAL().verificaCNPJexistente(CPFCNPJ) });
        }

        public JsonResult Alterar(int cod,string nome, string nacionalidade, string estadocivil, string RG, string CPFCNPJ, string rua, string bairro, string numero, int cidade, string CEP, string email, string contato, int representante, bool editchekCPF)
        {
            bool ok = false;
            Controls.ClienteControl ctl = new Controls.ClienteControl();

            ok = ctl.Alterar(cod, nome,nacionalidade,estadocivil,RG,CPFCNPJ,rua,bairro,numero,cidade,CEP,email,contato,representante, editchekCPF);

            return Json(new { ok });
        }
    }
}
