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
        public List<ViewModelOrdenDetalle> Items { get; private set; }
        public ActionResult Index()
        {
            if (TempData.ContainsKey("NotificationMessage"))
            {
                ViewBag.NotificationMessage = TempData["NotificationMessage"];
            }
            ViewBag.idUsaurio = listaUsuarios();

            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            return View();
        }

        private SelectList listaUsuarios()
        {
            IServiceUsuario _ServiceUsuario = new ServiceUsuario();
            IEnumerable<Usuario> listaUsuarios = _ServiceUsuario.Get_Usuario();

            return new SelectList(listaUsuarios, "IdUsuario", "IdUsuario");
        }

        public ActionResult ordenarZapato(int? idZapato)
        {
            int cantidadZapatos = Carrito.Instancia.Items.Count();
            ViewBag.NotiCarrito = Carrito.Instancia.AgregarItem((int)idZapato);
            return PartialView("_OrdenCantidad");

        }
        private ActionResult DetalleCarrito()
        {

            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }

        // GET: Autor/Create
        public ActionResult eliminarZapato(int? idZapato)
        {
            ViewBag.NotificationMessage = Carrito.Instancia.EliminarItem((int)idZapato);
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);
        }
        public ActionResult actualizarOrdenCantidad()
        {
            if (TempData.ContainsKey("NotiCarrito"))
            {
                ViewBag.NotiCarrito = TempData["NotiCarrito"];
            }
            int cantidadLibros = Carrito.Instancia.Items.Count();
            return PartialView("_OrdenCantidad");

        }
        public ActionResult actualizarCantidad(int idZapato, int cantidad)
        {
            ViewBag.DetalleOrden = Carrito.Instancia.Items;
            TempData["NotiCarrito"] = Carrito.Instancia.SetItemCantidad(idZapato, cantidad);
            TempData.Keep();
            return PartialView("_DetalleOrden", Carrito.Instancia.Items);

        }


        public ActionResult IndexAdmin()
        {
            IEnumerable<Entradas_Salidas> lista = null;

            try
            {
                IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
                lista = _ServiceEntradas_Salidas.GetEntradas_Salidas();
                return View(lista);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Details(int? id)
        {
            ServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
            Entradas_Salidas entradas_salidas = null;

            try
            {
                // Si va null
                if (id == null)
                {
                    return RedirectToAction("IndexAdmin");
                }

                entradas_salidas = _ServiceEntradas_Salidas.GetEntradas_SalidasByID(id.Value);
                if (entradas_salidas == null)
                {
                    TempData["Message"] = "No existe la orden solicitado";
                    TempData["Redirect"] = "Orden";
                    TempData["Redirect-Action"] = "IndexAdmin";
                    //TempData.Keep();
                    return RedirectToAction("Default", "Error");
                }
                return View(entradas_salidas);
            }
            catch (Exception ex)
            {
                // Salvar el error en un archivo 
                Log.Error(ex, MethodBase.GetCurrentMethod());
                TempData["Message"] = "Error al procesar los datos! " + ex.Message;
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "IndexAdmin";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }
        public ActionResult Save(Entradas_Salidas entradas_salidas)
        {

            try
            {

                // Si no existe la sesión no hay nada
                if (Carrito.Instancia.Items.Count() <= 0)
                {
                    // Validaciones de datos requeridos.
                    TempData["NotificationMessage"] = Util.SweetAlertHelper.Mensaje("Orden", "Seleccione los libros a ordenar", SweetAlertMessageType.warning);
                    return RedirectToAction("Index");
                }
                else
                {

                    var listaDetalle = Carrito.Instancia.Items;

                    foreach (var item in listaDetalle)
                    {
                        Entradas_Salidas entradas_Salidas = new Entradas_Salidas();
                        //entradas_Salidas.idZapato = item.idZapato;
                        entradas_Salidas.idUsuario = item.idUsuario;
                        entradas_Salidas.cantidadProducto = item.cantidadTotal;
                       // entradas_Salidas.Entradas_Salidas.Add(entradas_Salidas);
                    }
                }

                IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();
                Entradas_Salidas entradas_salidasSave = _ServiceEntradas_Salidas.Save(entradas_salidas);

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
                TempData["Redirect"] = "Orden";
                TempData["Redirect-Action"] = "Index";
                // Redireccion a la captura del Error
                return RedirectToAction("Default", "Error");
            }
        }


    }
}