using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RCDSystem.Controllers
{
    public class RegistrarArquivoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioArquivos()
        {
            return View();
        }

        public JsonResult Gravar()
        {

            bool ok = false;
            int cliente = Convert.ToInt32(Request.Form["cliente"]);
            int tipoarq = Convert.ToInt32(Request.Form["tipoarq"]);
            String descricao = Request.Form["descricao"];


            if (Request.Form.Files.Count > 0)
            {    

                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);

                byte[] arq = ms.ToArray();
                string nome = Request.Form.Files[0].FileName;
                string formato = System.IO.Path.GetExtension(nome);
                string tipo = Request.Form.Files[0].ContentType;
                int tamanho = Convert.ToInt32(Request.Form.Files[0].Length);

                Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();
                ok = ctl.Gravar(cliente,0, tipoarq, descricao, arq,nome,formato,tipo,tamanho);
            }

            


            return Json(new { ok });
        }

        public JsonResult GravarComProcesso()
        {

            bool ok = false;
            int cliente = Convert.ToInt32(Request.Form["cliente"]);
            int tipoarq = Convert.ToInt32(Request.Form["tipoarq"]);
            int processo = Convert.ToInt32(Request.Form["processo"]);
            String descricao = Request.Form["descricao"];


            if (Request.Form.Files.Count > 0)
            {

                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);

                byte[] arq = ms.ToArray();
                string nome = Request.Form.Files[0].FileName;
                string formato = System.IO.Path.GetExtension(nome);
                string tipo = Request.Form.Files[0].ContentType;
                int tamanho = Convert.ToInt32(Request.Form.Files[0].Length);

                Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();
                ok = ctl.Gravar(cliente, processo, tipoarq, descricao, arq, nome, formato, tipo, tamanho);
            }




            return Json(new { ok });
        }

        public JsonResult Alterar()
        {
            bool ok = false;
            int codigo = Convert.ToInt32(Request.Form["codigo"]);

            int cliente = Convert.ToInt32(Request.Form["cliente"]);
            int tipoarq = Convert.ToInt32(Request.Form["tipoarq"]);
            int processo = Convert.ToInt32(Request.Form["processo"]);
            String descricao = Request.Form["descricao"];

            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            if (Request.Form.Files.Count > 0)
            {

                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);

                byte[] arq = ms.ToArray();
                string nome = Request.Form.Files[0].FileName;
                string formato = System.IO.Path.GetExtension(nome);
                string tipo = Request.Form.Files[0].ContentType;
                int tamanho = Convert.ToInt32(Request.Form.Files[0].Length);

               
                ok = ctl.Alterar(codigo,cliente, processo, tipoarq, descricao, arq, nome, formato, tipo, tamanho);
            }
            else
            {
                

                Models.Arquivo dados = new DAL.RegistrarArquivoDAL().GetArquivo2(codigo);
                byte[] arq = dados.Arq;
                string nome = dados.Nome;
                string formato = dados.Formato;
                string tipo = dados.Tipo;
                int tamanho = dados.Tamanho;

                
                ok = ctl.Alterar(codigo,cliente, processo, tipoarq, descricao, arq, nome, formato, tipo, tamanho);

            }




            return Json(new { ok });
        }


        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            List<Models.Arquivo> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult ObterTabela()
        {
            bool ok = false;
            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            List<Models.Arquivo> dados = ctl.ObterTodos();

            return Json(new {arquivos = new DAL.RegistrarArquivoDAL().ObterTodos()});
        }

        public JsonResult ObterTabelaProcesso(int cod)
        {
            return Json(new { arquivos = new DAL.RegistrarArquivoDAL().ObterArquivosProcesso(cod) });
        }




        public JsonResult Excluir(int cod)
        {
            bool ok = false;
            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            ok = ctl.Excluir(cod);

            return Json(new { ok });
        }

        public IActionResult ViewArquivo(int cod)
        {
            bool ok = false;
            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            Models.Arquivo dados = ctl.GetArquivo(cod);
            return File(dados.Arq, dados.Tipo);
            //return File(dados.Arq, dados.Tipo,"arquivo.jpg");
        }

        public IActionResult GetArquivo(int cod)
        {
            bool ok = false;
            Controls.RegistrarArquivoControl ctl = new Controls.RegistrarArquivoControl();

            Models.Arquivo dados = ctl.GetArquivo(cod);
            


            return File(dados.Arq, dados.Tipo,dados.Nome);
        }



        public JsonResult LoadInicial()
        {
            return Json(new { cliente = new DAL.ClienteDAL().ObterTodos(),
                tipoarq = new DAL.TipoArquivoDAL().ObterTodos(),
                arquivos = new DAL.RegistrarArquivoDAL().ObterTodos()});
        }
        public JsonResult LoadRelatorio()
        {
            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                tipoarq = new DAL.TipoArquivoDAL().ObterTodos(),
                processo = new DAL.ProcessoDAL().ObterProcessoComArq()
            });
        }


        private Reports.ArquivoReports getRelatorio(int tipodes, int tipoarq, int processo)
        {
            var rpt = new Reports.ArquivoReports(tipodes, tipoarq,processo);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Arquivos";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int cliente, int tipoarq, int processo)
        {
            var rpt = getRelatorio(cliente, tipoarq, processo);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }

    }
}