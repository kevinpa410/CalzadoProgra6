using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
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
    }
}