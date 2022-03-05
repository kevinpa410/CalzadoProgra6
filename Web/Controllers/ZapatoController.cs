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
    }
}