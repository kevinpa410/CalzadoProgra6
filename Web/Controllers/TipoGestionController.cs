using ApplicationCore.Services;
using Infrastructure.Models;
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
    public class TipoGestionController : Controller
    {
        // GET: TipoGestion
        public ActionResult Index()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
           // ViewBag.idCliente = listaClientes();

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }
        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }
        public ActionResult actualizarCantidad(int idZapato, int cantidadTotal)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idZapato, cantidadTotal);
            TempData.Keep();
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);

        }
        public ActionResult ordenarZapato(int? idZapato)
        {
            int cantidadZapatos = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idZapato);
            return PartialView("_OrdenCantidad");

        }
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadZapatos = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        public ActionResult eliminarZapato(int? idZapato)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((int)idZapato);
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }
        // GET: TipoGestion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult IndexAdmin()
        {
            IEnumerable<TipoGestion> lista = null;

            try
            {
                IServiceTipoGestion _ServiceTipoGestion = new ServiceTipoGestion();
                lista = _ServiceTipoGestion.GetTipoGestion();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoGestion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Details(int? id)
        {
            ServiceTipoGestion _ServiceTipoGestion = new ServiceTipoGestion();
            TipoGestion tipoGestion = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                tipoGestion = _ServiceTipoGestion.GetTipoGestionByID(id.Value);
                if (tipoGestion == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "TipoGestion";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(tipoGestion);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoGestion";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Save(TipoGestion tipoGestion)
        {

            try
            {

                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Seleccione los zapatos a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = Carrito.Instancia.Items;

                    foreach (var item in listaDetalle)
                    {
                        Entradas_Salidas entradas_salidas = new Entradas_Salidas();
                        entradas_salidas.idZapato = item.idZapato;
                        entradas_salidas.fecha = item.fecha;
                        entradas_salidas.cantidadTotal = item.cantidadTotal;
                        tipoGestion.Entradas_Salidas.Add(entradas_salidas);
                    }
                }

                IServiceTipoGestion _ServiceTipoGestion = new ServiceTipoGestion();
                TipoGestion tipoGestionSave = _ServiceTipoGestion.Save(tipoGestion);

                // Limpia el Carrito de compras
                Carrito.Instancia.eliminarCarrito();
                TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Orden guardada satisfactoriamente!", SweetAlertMessageType.success);
                // Reporte orden
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Salvar el error  
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "TipoGestion";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }

       
    }
}
