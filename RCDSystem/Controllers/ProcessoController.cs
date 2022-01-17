using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCDSystem.Controllers
{
    public class ProcessoController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult RelatorioProcessos()
        {
            return View();
        }


        public JsonResult Gravar(int cod, string descricao, int cliente, int categoria, decimal valortotal, string observacoes,string numregistro)
        {


            bool ok = false;

            Controls.ProcessoControl ctl = new Controls.ProcessoControl();
            ok = ctl.Gravar(cod, descricao, cliente, categoria, valortotal, observacoes,numregistro);

            return Json(new
            {
                ok,
                codigo = new DAL.ProcessoDAL().ObterCodNovo(descricao)
            });
        }

        public JsonResult GeraProcesso(int cod, string desc, int cliente, int categoria, decimal valorprocesso, string observacoes, string numregistro)
        {
            int codprot = cod;
            cod = 0;

            bool ok = false;

            Controls.ProcessoControl ctl = new Controls.ProcessoControl();
            ok = ctl.Gravar(cod, desc, cliente, categoria, valorprocesso, observacoes, numregistro);

            if(ok == true)
            {   
                int processcod = new DAL.ProcessoDAL().ObterCodNovo(desc);
                new DAL.ProtocoloDAL().GravaProcesso(codprot, processcod);
            }

            return Json(new
            {
                ok,
                codigo = new DAL.ProcessoDAL().ObterCodNovo(desc)
            });
        }

        public JsonResult LoadBusca()
        {

            return Json(new
            {
                processo = new DAL.ProcessoDAL().ObterTodos()
            });
        }

        public JsonResult ValidaDescricaoProcesso(string descricao)
        {

            return Json(new
            {
                ok = new DAL.ProcessoDAL().ValidaDescricao(descricao)
            }); ; ;
        }

        public JsonResult ValidaDescricaoArquivo(string descricao, int processocod)
        {

            return Json(new
            {
                ok = new DAL.RegistrarArquivoDAL().ValidaDescricao(descricao, processocod)
            }); ; ;
        }

        public JsonResult Excluir(int cod)
        {
            bool ok = false;



                Controls.ProcessoControl ctl = new Controls.ProcessoControl();
                ok = ctl.Excluir(cod);

            if (ok == true)
                new DAL.RegistrarArquivoDAL().ExcluirArqProcesso(cod);



            return Json(new
            {
                ok
            });
        }

        public JsonResult LoadArquivo(int cod)
        {

            return Json(new
            {
                arquivo = new DAL.RegistrarArquivoDAL().ObterArquivosProcesso(cod)
            }); ;
        }

        public JsonResult LoadRelatorio()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                status = new DAL.StatusPPDAL().ObterTodos()
            });
        }


        public JsonResult LoadInicial()
        {

            return Json(new
            {
                cliente = new DAL.ClienteDAL().ObterTodos(),
                categoria = new DAL.CategoriaPPDAL().ObterTodos(),
                tipoarq = new DAL.TipoArquivoDAL().ObterTodos()
            });
        }

        private Reports.ProcessoReports getRelatorio(int tipodes, int status, string data_i, string data_f)
        {
            var rpt = new Reports.ProcessoReports(tipodes, status, data_i, data_f);
            rpt.BasePath = System.IO.Directory.GetCurrentDirectory();

            rpt.PageTitle = "Relatório de Processos";
            rpt.ImprimirCabecalhoPadrao = true;
            rpt.ImprimirRodapePadrao = true;

            return rpt;
        }

        public ActionResult Preview(int cliente, int status, string data_i, string data_f)
        {
            var rpt = getRelatorio(cliente, status, data_i, data_f);

            return File(rpt.GetOutput().GetBuffer(), "application/pdf");
        }


    }
}
