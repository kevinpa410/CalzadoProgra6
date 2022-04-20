using ApplicationCore.Services;
using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Web.Util;
using Web.ViewModel;

namespace Web.Controllers
{
    public class Entradas_SalidasController : Controller
    {
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
                    TempData["Message"] = "NO EXISTE EL MOVIMIENTO SOLICITADO";
                    TempData["Redirect"] = "Entradas_Salidas";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");

                }
                return View(entradas_salidas);
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Entradas_Salidas";
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
                return new MultiSelectList(listaZapatos, "idZapato", "descripcion", listaZapatosSelect);
        }
        public ActionResult Create()
        {
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
                if(entradas_salidas == null)
                {
                    TempData["Message"] = "NO EXISTE EL MOVIMIENTO SOLICITADO";
                    TempData["Redirect"] = "Entradas_Salidas";
                    TempData["Redirect"] = "IndexAdmin";
                    return RedirectToAction("Default", "Error");
                }
                return View(entradas_salidas);
            }
            catch(Exception ex)
            {
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Entradas_Salidas";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        private dynamic listaZapato(Zapato zapato)
        {
            throw new NotImplementedException();
        }
        public ActionResult Save(Entradas_Salidas entradas_salidas, String[] selectedZapatos)
        {
            IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
            if (ModelState.IsValid)
            {
                Entradas_Salidas oEntradas_SalidasI = _ServiceEntradas_Salidas.Save(entradas_salidas);
            }
            else
            {
                ViewBag.idZapato = listaZapato(entradas_salidas.Zapato);
                return View("Create", entradas_salidas);
            }
            return RedirectToAction("IndexAdmin");
        }

    }
}