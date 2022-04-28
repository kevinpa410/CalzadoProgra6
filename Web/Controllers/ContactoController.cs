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
    public class ContactoController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Contacto> lista = null;
            try
            {
                IServiceContacto _ServiceContacto = new ServiceContacto();
                lista = _ServiceContacto.GetContacto();
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
            IEnumerable<Contacto> lista = null;
            try
            {
                IServiceContacto _ServiceContacto = new ServiceContacto();
                lista = _ServiceContacto.GetContacto();

            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;

                return RedirectToAction("Default", "Error");
            }
            return View(lista);
        }
        public ActionResult Details(int? id)
        {
            IServiceContacto _IServiceContacto = new ServiceContacto();
            Contacto contacto = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");

                }
                contacto = _IServiceContacto.GetContactoByID(id.Value);
                if (contacto == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Contacto";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(contacto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Contacto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        private MultiSelectList listaContacto(ICollection<Contacto> contacto)
        {
            IServiceContacto _ServiceContacto = new ServiceContacto();
            IEnumerable<Contacto> listacontacto = _ServiceContacto.GetContacto();
            int[] listacontactoSelect = null;
            if (contacto != null)
            {
                listacontactoSelect = contacto.Select(c => c.idContacto).ToArray();
            }
            return new MultiSelectList(listacontacto, "idCategoria", "Nombre", listacontactoSelect);

        }
        private SelectList listaProveedor(int idProveedor = 0)
        {
            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            IEnumerable<Proveedor> listaProveedor = _ServicesProveedor.GetProveedor();
            //Proveedor SelectProveedor = listaProveedor.Where(c => c.idProveedor == idProveedor).FirstOrDefault();
            return new SelectList(listaProveedor, "idProveedor", "nombre", idProveedor);

        }
        public ActionResult Create()
        {
            ViewBag.idProveedor = listaProveedor();
            ViewBag.idCategoria = listaContacto(null);
            return View();
        }
        public ActionResult Edit(int? id)
        {
            ServiceContacto _ServiceContacto = new ServiceContacto();
            Contacto contacto = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                contacto = _ServiceContacto.GetContactoByID(id.Value);
                if (contacto == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "contacto";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                //ViewBag.idProveedor = listaProveedor(zapato.idProveedor);
                //ViewBag.idCategoria = listaCategorias(zapato.Categoria);
                return View(contacto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "contacto";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Save(Contacto contacto)
        {

            IServiceContacto _ServiceContacto = new ServiceContacto();

            if (ModelState.IsValid)
            {
                Contacto oContacto = _ServiceContacto.Save(contacto);
            }
            else
            {
                // Valida Errores si Javascript está deshabilitado
                //Util.ValidateErrors(this);
                ViewBag.idProveedor = listaProveedor();
                return View("Create", contacto);
            }
            return RedirectToAction("IndexAdmin");

        }

    }
}