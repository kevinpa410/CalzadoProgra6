using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Utils;

namespace Web.Controllers
{

    public class Entradas_SalidasController : Controller
    {
        // GET: Zapato
        public ActionResult Index()
        {
            IEnumerable<Entradas_Salidas> lista = null;
            try
            {
                IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
                lista = _ServiceEntradas_Salidas.GetEntradas_Salidas();
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
            IEnumerable<Entradas_Salidas> lista = null;
            try
            {
                IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
                lista = _ServiceEntradas_Salidas.GetEntradas_Salidas();

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
            IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
            Entradas_Salidas entradas_salidas = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                entradas_salidas = _ServiceEntradas_Salidas.GetEntradas_SalidasByID(id.Value);
                if (entradas_salidas == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Zapato";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(entradas_salidas);
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
        private MultiSelectList listaZapatos(ICollection<Zapato> zapato)
        {
            IServiceZapato _ServiceZapato = new ServiceZapato();
            IEnumerable<Zapato> listaZapatos = _ServiceZapato.GetZapato();
            int[] listaZapatosSelect = null;
            if (zapato != null)
            {
                listaZapatosSelect = zapato.Select(c => c.idZapato).ToArray();
            }
            return new MultiSelectList(listaZapatos, "idZapato", "Nombre", listaZapatosSelect);

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
            ViewBag.idZapato = listaZapatos(null);
            return View();

        }
        public ActionResult Edit(int? id)
        {
            ServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
            Entradas_Salidas entradas_salidas = null;
            try
            {
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");

                }
                entradas_salidas = _ServiceEntradas_Salidas.GetEntradas_SalidasByID(id.Value);
                if (entradas_salidas == null)
                {
                    TempData["Message"] = "NO EXISTE EL ZAPATO SOLICITADO";
                    TempData["Redirect"] = "Zapato";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                //ViewBag.idProveedor = listaProveedor(zapato.idProveedor);
                //ViewBag.idCategoria = listaCategorias(zapato.Categoria);
                return View(entradas_salidas);
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

        public ActionResult Save(Entradas_Salidas entradas_salidas, string[] selectedCategorias)
        {

            IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();

            if (ModelState.IsValid)
            {
                Entradas_Salidas oEntradas_SalidasI = _ServiceEntradas_Salidas.Save(entradas_salidas);
            }
            else
            {
                // Valida Errores si Javascript está deshabilitado
                //Util.ValidateErrors(this);
                ViewBag.idProveedor = listaProveedor();
                ViewBag.idZapato = listaZapatos(entradas_salidas.Zapato);
                return View("Create", entradas_salidas);
            }
            return RedirectToAction("IndexAdmin");

        }
    }

}  
            

     
