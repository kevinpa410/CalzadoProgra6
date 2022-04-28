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
    public class ProveedorController : Controller
    {

        public ActionResult Index()
        {
            IEnumerable<Proveedor> lista = null;
            try
            {
                IServicesProveedor _SeviceProveedor = new ServicesProveedor();
                lista = _SeviceProveedor.GetProveedor();
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
            }
            return View(lista);
        }
        
        private MultiSelectList listaZapatos(ICollection<Zapato> zapatos)
        {
            //Lista de Categorias
            IServiceZapato _ServiceZapato = new ServiceZapato();
            IEnumerable<Zapato> listaZapato = _ServiceZapato.GetZapato();
            int[] listaZapatoSelect = null;

            if (zapatos != null)
            {

                listaZapatoSelect = zapatos.Select(c => c.idZapato).ToArray();
            }

            return new MultiSelectList(listaZapato, "idZapato", "descripcion", listaZapatoSelect);
        }

        private MultiSelectList listaContactos(ICollection<Contacto> contactos)
        {
            //Lista de Categorias
            IServiceContacto _ServiceContacto = new ServiceContacto();
            IEnumerable<Contacto> listaContacto = _ServiceContacto.GetContacto();
            int[] listaContactoSelect = null;

            if (contactos != null)
            {

                listaContactoSelect = contactos.Select(c => c.idContacto).ToArray();
            }

            return new MultiSelectList(listaContacto, "idContacto", "nombre", listaContactoSelect);

        }

        public ActionResult Create()
        {
            //Lista de Zapatos y Contactos
            ViewBag.idZapato = listaZapatos(null);
            ViewBag.idContacto = listaContactos(null);
            return View();
        }

        public ActionResult Edit(int? id)
        {

            ServicesProveedor _ServicesProveedor = new ServicesProveedor();
            Proveedor proveedor = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                proveedor = _ServicesProveedor.GetProveedorByID(id.Value);
                if (proveedor == null)
                {
                    TempData["Message"] = "NO EXISTE EL PROVEEDOR SOLICITADO";
                    TempData["Redirect"] = "Proveedor";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.idZapato = listaZapatos(proveedor.Zapato);
                ViewBag.idContacto = listaContactos(proveedor.Contacto);
                return View(proveedor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Zapato";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
            
        public ActionResult IndexAdmin()
        {
            IEnumerable<Proveedor> lista = null;
            try
            {
                IServicesProveedor _ServicesProveedor = new ServicesProveedor();
                lista = _ServicesProveedor.GetProveedor();

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
            ServicesProveedor _ServicesProveedor = new ServicesProveedor();
            Proveedor proveedor = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                proveedor = _ServicesProveedor.GetProveedorByID(id.Value);
                if (proveedor == null)
                {
                    TempData["Message"] = "NO EXISTE EL PROVEEDOR SOLICITADO";
                    TempData["Redirect"] = "Proveedor";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(proveedor);
            }
            catch (Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Zapato";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult Delete(int id)
        {
            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            if (ModelState.IsValid)
            {
                _ServicesProveedor.DeleteProveedor(id);
            }
            return RedirectToAction("IndexAdmin");

        }

        public ActionResult Save(Proveedor proveedor, string[] selectedContactos, string[] selectedZapatos)
        {
            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            if (ModelState.IsValid)
            {
                Proveedor oProvedorI = _ServicesProveedor.Save(proveedor, selectedContactos, selectedZapatos);
            }
            else
            {
                ViewBag.idZapato = listaZapatos(proveedor.Zapato);
                ViewBag.idCategoria = listaContactos(proveedor.Contacto);
                return View("Create", proveedor);
            }
            return RedirectToAction("IndexAdmin");
        }


        }
    }