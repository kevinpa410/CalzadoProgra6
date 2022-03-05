using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(Usuario usuario)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario oUsuario = null;
            try
            {
                if (ModelState.IsValid)
                {
                    oUsuario = _ServiceUsuario.Get_Usuario(usuario.email, usuario.password);

                    if (oUsuario != null)
                    {
                        Session["User"] = oUsuario;
                        Log.Info($"Accede {oUsuario.nombre} {oUsuario.apellidos} con el rol {oUsuario.Rol.idRol}-{oUsuario.Rol.descripcion}");
                        TempData["mensaje"] = Util.SweetAlertHelper.Mensaje("Login", "Usario autenticado satisfactoriamente", SweetAlertMessageType.success);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        Log.Warn($"{usuario.email} se intentó conectar  y falló");
                        ViewBag.NotificationMessage = Util.SweetAlertHelper.Mensaje("Login", "Error al autenticarse", SweetAlertMessageType.warning);

                    }
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData.Keep();
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult UnAuthorized()
        {
            try
            {
                ViewBag.Message = "No autorizado";

                if (Session["User"] != null)
                {
                    Usuario oUsuario = Session["User"] as Usuario;
                    Log.Warn($"El usuario {oUsuario.nombre} {oUsuario.apellidos} con el rol {oUsuario.Rol.idRol}-{oUsuario.Rol.descripcion}, intentó acceder una página sin permisos  ");
                }

                return View();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Logout()
        {
            try
            {
                Log.Info("Usuario desconectado!");
                Session["User"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                // Pasar el Error a la página que lo muestra
                TempData["Message"] = ex.Message;
                TempData["Redirect"] = "Login";
                TempData["Redirect-Action"] = "Index";
                return RedirectToAction("Default", "Error");
            }
        }
    }
}