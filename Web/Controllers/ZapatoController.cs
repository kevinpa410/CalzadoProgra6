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
    public class ZapatoController : Controller
    {
        // GET: Zapato
        public ActionResult Index()
        {
            IEnumerable<Zapato> lista = null;
            try
            {
                IServiceZapato _SeviceZapato = new ServiceZapato();
                lista = _SeviceZapato.GetZapato();
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
            IEnumerable<Zapato> lista = null;
            try
            {
                IServiceZapato _ServiceZapato = new ServiceZapato();
                lista = _ServiceZapato.GetZapato();

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
            ServiceZapato _ServiceZapato = new ServiceZapato();
            Zapato zapato = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                zapato = _ServiceZapato.GetZapatoByID(id.Value);
                if (zapato == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Zapato";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(zapato);
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
        private SelectList listaCategoria (int idCategoria = 0 )
        {
            IServiceCategoria _ServicesCategoria = new ServiceCategoria();
            IEnumerable<Categoria> listaCategoria = _ServicesCategoria.GetCategoria();
            //Proveedor SelectProveedor = listaProveedor.Where(c => c.idProveedor == idProveedor).FirstOrDefault();
            return new SelectList(listaCategoria, "idCategoria", "nombre", idCategoria);

        }
        private MultiSelectList listaUbicacion(ICollection<Ubicacion> ubicacion)
        {
            IServiceUbicacion _ServicesUbicacion = new ServiceUbicacion();
            IEnumerable<Ubicacion> listaUbicacion = _ServicesUbicacion.GetUbicacion();
            int[] listaUbicacionSelect = null;
            if (ubicacion != null)
            {
                listaUbicacionSelect = ubicacion.Select(c => c.idUbicacion).ToArray();
            }
            return new MultiSelectList(listaUbicacion, "idUbicacion", "descripcion", listaUbicacionSelect);
        } 
        private MultiSelectList listaProveedor(ICollection<Proveedor> proveedor)
        {

            IServicesProveedor _ServicesProveedor = new ServicesProveedor();
            IEnumerable<Proveedor> listaProveedor = _ServicesProveedor.GetProveedor();
            int[] listaProveedorSelect = null;
            if (proveedor != null)
            {
                listaProveedorSelect = proveedor.Select(c => c.idProveedor).ToArray();
            }
            return new MultiSelectList(listaProveedor, "idProveedor", "nombre", listaProveedorSelect);
                        

        }
        public ActionResult Create()
        {
            ViewBag.idUbicacion = listaUbicacion(null);
            ViewBag.idProveedor = listaProveedor(null);
            ViewBag.idCategoria = listaCategoria();
            return View();

        }       
        public ActionResult Edit(int? id)
        {
            ServiceZapato _ServiceZapato = new ServiceZapato();
            Zapato zapato = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                zapato = _ServiceZapato.GetZapatoByID(id.Value);
                if (zapato == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Zapato";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                ViewBag.idUbicacion = listaUbicacion(zapato.Ubicacion);
                ViewBag.IdCategoria = listaCategoria(zapato.idCategoria);
                ViewBag.idProveedor = listaProveedor(zapato.Proveedor);

                return View(zapato);
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
        public ActionResult Save(Zapato zapato, string[] selectedProveedor, string[] selectedUbicacion)
        {

            IServiceZapato _ServiceZapato = new ServiceZapato();

            if (ModelState.IsValid)
            {
                Zapato oZapatoI = _ServiceZapato.Save(zapato, selectedProveedor, selectedUbicacion);
            }
            else
            {

                ViewBag.idProveedor = listaProveedor(zapato.Proveedor);
                ViewBag.idCategoria = listaCategoria(zapato.idCategoria);
                ViewBag.idUbicacion = listaUbicacion(zapato.Ubicacion);

                return View("Create", zapato);
            }
            return RedirectToAction("IndexAdmin");

        }

    }
}

     