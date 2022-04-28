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
        public ActionResult Create()
        {
            ViewBag.idUbicacion = listaUbicacion(null);
            ViewBag.idGestion = listaTipoGestion();
            ViewBag.idZapato = listaZapatos();

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

        //Listas
        private SelectList listaTipoGestion(int idTipoGestion = 0)
        {
            IServiceTipoGestion _ServicesTipoGestion = new ServiceTipoGestion();
            IEnumerable<TipoGestion> listaServiceTipoGestion = _ServicesTipoGestion.GetTipoGestion();
            return new SelectList(listaServiceTipoGestion, "idGestion", "Descripcion", idTipoGestion);
        }
        private SelectList listaZapatos(int idZapatos = 1)
        {
            IServiceZapato _ServicesZapatos = new ServiceZapato();
            IEnumerable<Zapato> listaZapatos = _ServicesZapatos.GetZapato();
            return new SelectList(listaZapatos, "idZapato", "descripcion", idZapatos);
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
        public ActionResult Save(Entradas_Salidas entradas_salidas, string[] selectedUbicacion)
        {

            IServiceEntradas_Salidas _ServiceEntradas_Salidas = new ServiceEntradas_Salidas();

            if (ModelState.IsValid)
            {
                Entradas_Salidas oEntradas_Salidas = _ServiceEntradas_Salidas.Save(entradas_salidas, selectedUbicacion);
            }
            else
            {
                ViewBag.idGestion = listaTipoGestion((int)entradas_salidas.idGestion);
                ViewBag.idZapato = listaZapatos((int)entradas_salidas.idZapato);

                return View("Create", entradas_salidas);
            }
            return RedirectToAction("IndexAdmin");
        }


    }
}