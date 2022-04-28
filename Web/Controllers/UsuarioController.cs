using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{
    public class UsuarioController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 

                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<Usuario> lista = null;
            try
            {
                IServiceUsuario _ServiceUsuario = new ServiceUsuario();
                lista = _ServiceUsuario.GetUsuarios();

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }
        public ActionResult Create()
        {
            ViewBag.idRol = listaRol();
            return View();

        }
        public ActionResult Edit(int? id)
        {
            ServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                usuario = _ServiceUsuario.GetUsuarioByID(id.Value);
                if (usuario == null)
                {
                    TempData["Message"] = "NO EXISTE EL Rol SOLICITADO";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.idRol = listaRol();

                return View(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Details(int? id)
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            Usuario usuario = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                usuario = _ServiceUsuario.GetUsuarioByID(id.Value);
                if (usuario == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Usuario";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        private SelectList listaRol(int idRol = 0)
        {
            IServiceRol _ServicesRol = new ServiceRol();
            IEnumerable<Rol> listaRol = _ServicesRol.GetRol();
            //Proveedor SelectProveedor = listaProveedor.Where(c => c.idProveedor == idProveedor).FirstOrDefault();
            return new SelectList(listaRol, "idRol", "descripcion", idRol);

        }
        public ActionResult Save(Usuario usuario)
        {
            MemoryStream target = new MemoryStream();
            IServiceUsuario _Serviceusuario = new ServiceUsuario();
            try
            {

                if (ModelState.IsValid)
                {
                    Usuario oUsuario = _Serviceusuario.Save(usuario);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.Util.ValidateErrors(this);
                    ViewBag.idRol = listaRol(usuario.idRol);

                    return View("Create", usuario);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Usuario";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


    }
}