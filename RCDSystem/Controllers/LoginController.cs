using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RCDSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult CadastroUsuario()
        {
            return View();
        }

        public JsonResult Gravar(int codigo,string usuario,string senha)
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            ok = ctl.Gravar(codigo, usuario, senha);

            return Json(new { ok });
        }
        public JsonResult AtivaUsuario(int codigo)
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            ok = ctl.AtivaUsuario(codigo);

            return Json(new { ok });
        }

        public JsonResult DesativaUsuario(int codigo)
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            ok = ctl.DesativaUsuario(codigo);

            return Json(new { ok });
        }


        public JsonResult ObterTodos()
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            List<Models.Login> dados = ctl.ObterTodos();

            return Json(new { dados });
        }

        public JsonResult ValidaUsuario(string usuario,string senha)
        {
            bool u = false;
            bool s = false;
            bool a = false;

            s = new DAL.LoginDAL().ValidaUsuarioSenha(usuario,senha);
            if(s == true)
            {
                u = true;
                a = new DAL.LoginDAL().VerificaAtivo(usuario);
            }
            else
            {
                if(new DAL.LoginDAL().ValidaUsuario(usuario) != null)
                {
                    u = true;
                    a = new DAL.LoginDAL().VerificaAtivo(usuario);
                }
                
            }

            return Json(new { u,s,a});
        }

        public JsonResult Excluir(int cod_tipo)
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            ok = ctl.Excluir(cod_tipo);

            return Json(new { ok });
        }

        public JsonResult Alterar(int codigo, string usuario, string senha,bool ativo)
        {
            bool ok = false;
            Controls.LoginControl ctl = new Controls.LoginControl();

            ok = ctl.Alterar(codigo,usuario,senha,ativo);

            return Json(new { ok });
        }

        public async Task<JsonResult> LogarAsync(string usuario)
        {

                var claims = new List<Claim>();
                claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, usuario),
                        new Claim(ClaimTypes.Role, "Administrador")
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

                return Json(true);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

    }
}