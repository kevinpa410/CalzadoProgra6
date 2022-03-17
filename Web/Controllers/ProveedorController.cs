using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ActionResult Create()
        {
            //Lista de autores
            ViewBag.IdProveedor = listaProveedores();
            return View();
        }

        public ActionResult Save(Proveedor proveedor, HttpPostedFileBase ImageFile)
        {
            MemoryStream target = new MemoryStream();
            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            try
            {
                // Cuando es Insert Image viene en null porque se pasa diferente
                //if (proveedor.Imagen == null)
                //{
                //    if (ImageFile != null)
                //    {
                //        ImageFile.InputStream.CopyTo(target);
                //        proveedor.Imagen = target.ToArray();
                //        ModelState.Remove("Imagen");
                //    }

                //}
                if (ModelState.IsValid)
                {
                    Proveedor oProveedor = _ServicesProveedor.Save(proveedor);
                }
                else
                {
                    // Valida Errores si Javascript está deshabilitado
                    Util.Util.ValidateErrors(this);
                    ViewBag.IdAutor = listaProveedores(proveedor.idProveedor);
                    return View("Create", proveedor);
                }

                return RedirectToAction("IndexAdmin");
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        public ActionResult Edit(int? id)
        {
            ServicesProveedor _ServicesProveedor = new ServicesProveedor();
            Proveedor proveedor = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                proveedor = _ServicesProveedor.GetProveedorByID(id.Value);
                if (proveedor == null)
                {
                    TempData["Message"] = "No existe el libro solicitado";
                    TempData["Redirect"] = "Proveedor";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    // Redireccion a la captura del Error
                    return RedirectToAction("Default", "Error");
                }
                //Lista de autores
                ViewBag.idProveedor = listaProveedores(proveedor.idProveedor);
                return View(proveedor);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        private SelectList listaProveedores(int idProveedor = 0)
        {
            //Lista de autores
            IServicesProveedor _ServiceProveedor = new ServicesProveedor();
            IEnumerable<Proveedor> listaProveedores = _ServiceProveedor.GetProveedor();
            //Proveedor SelectProveedor = listaProveedor.Where(c => c.idProveedor == idProveedor).FirstOrDefault();
            return new SelectList(listaProveedores, "idProveedor", "nombre", idProveedor);
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
                TempData["Redirect"] = "Proveedor";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

        }
    }