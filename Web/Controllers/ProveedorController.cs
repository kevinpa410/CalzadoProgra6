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


        // GET: Autor/Create
        public ActionResult Create()
        {
            ViewBag.idZapato= listaZapato();
            return View();
        }

     
        private SelectList listaZapato(int idZapato = 0)
        {
            IServiceZapato _ServiceZapato = new ServiceZapato();
            IEnumerable<Zapato> listaZapato = _ServiceZapato.GetZapato();
            //Proveedor SelectProveedor = listaProveedor.Where(c => c.idProveedor == idProveedor).FirstOrDefault();
            return new SelectList(listaZapato, "idZapato", "nombre", idZapato);

        }

        // GET: Autor/Edit/5
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
                //ViewBag.idZapato = listaZapato(proveedor.idContacto);
                //ViewBag.idCategoria = listaCategorias(zapato.Categoria);
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

        // GET: Autor/Details/5
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

        // GET: Autor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
        public ActionResult Save(Proveedor proveedor)
        {
            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            if (ModelState.IsValid)
            {
                Proveedor oProvedorI = _ServicesProveedor.Save(proveedor);

            }
            else
            {
                ViewBag.idZapato = listaZapato();
                return View("Create", proveedor);
            }
            return RedirectToAction("IndexAdmin");
        }


        }
    }