﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Util;

namespace Web.ViewModel
{
    public class Carrito
    {
        public List<ViewModelOrdenDetalle> Items { get; private set; }
       

        //Implementación Singleton

        // Las propiedades de solo lectura solo se pueden establecer en la inicialización o en un constructor
        public static readonly Carrito Instancia;

        // Se llama al constructor estático tan pronto como la clase se carga en la memoria
        static Carrito()
        {
            // Si el carrito no está en la sesión, cree uno y guarde los items.
            if (HttpContext.Current.Session["carrito"] == null)
            {
                Instancia = new Carrito();
                Instancia.Items = new List<ViewModelOrdenDetalle>();
                HttpContext.Current.Session["carrito"] = Instancia;
            }
            else
            {
                // De lo contrario, obténgalo de la sesión.
                Instancia = (Carrito)HttpContext.Current.Session["carrito"];
            }
        }

        // Un constructor protegido asegura que un objeto no se puede crear desde el exterior
        protected Carrito() { }

        /**
         * AgregarItem (): agrega un artículo a la compra
         */
        public String AgregarItem(int zapatoId)
        {
            String mensaje = "";
            // Crear un nuevo artículo para agregar al carrito
            ViewModelOrdenDetalle nuevoItem = new ViewModelOrdenDetalle(zapatoId);
            // Si este artículo ya existe en lista de libros, aumente la Cantidad
            // De lo contrario, agregue el nuevo elemento a la lista
            if (nuevoItem != null)
            {
                if (Items.Exists(x => x.idZapato == zapatoId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.idZapato == zapatoId);
                    item.cantidadTotal++;
                }
                else
                {
                    nuevoItem.cantidadTotal = 1;
                    Items.Add(nuevoItem);
                }
                mensaje = SweetAlertHelper.Mensaje("Orden Zapato", "Zapato agregado a la orden", SweetAlertMessageType.success);

            }
            else
            {
                mensaje = SweetAlertHelper.Mensaje("Orden Zapato", "El Zapato solicitado no existe", SweetAlertMessageType.warning);
            }
            return mensaje;
        }


        /**
         * SetItemCantidad(): cambia la Cantidad de un artículo en el carrito
         */
        public String SetItemCantidad(int zapatoId, int cantidadTotal)
        {
            String mensaje = "";
            // Si estamos configurando la Cantidad a 0, elimine el artículo por completo
            if (cantidadTotal == 0)
            {
                EliminarItem(zapatoId);
                mensaje = SweetAlertHelper.Mensaje("Orden Zapato", "Zapato eliminado", SweetAlertMessageType.success);

            }
            else
            {
                // Encuentra el artículo y actualiza la Cantidad
                ViewModelOrdenDetalle actualizarItem = new ViewModelOrdenDetalle(zapatoId);
                if (Items.Exists(x => x.idZapato == zapatoId))
                {
                    ViewModelOrdenDetalle item = Items.Find(x => x.idZapato == zapatoId);
                    item.cantidadTotal = cantidadTotal;
                    mensaje = SweetAlertHelper.Mensaje("Orden zapato", "Cantidad actualizada", SweetAlertMessageType.success);

                }
            }
            return mensaje;

        }

        /**
         * EliminarItem (): elimina un artículo del carrito de compras
         */
        public String EliminarItem(int zapatoId)
        {
            String mensaje = "El zapato no existe";
            if (Items.Exists(x => x.idZapato == zapatoId))
            {
                var itemEliminar = Items.Single(x => x.idZapato == zapatoId);
                Items.Remove(itemEliminar);
                mensaje = SweetAlertHelper.Mensaje("Orden Zapato", "zapato eliminado", SweetAlertMessageType.success);
            }
            return mensaje;

        }

        /**
         * GetTotal() - Devuelve el precio total de todos los libros.
         */
        public int GetCountItems()
        {
            int total = 0;
            total = Items.Sum(x => x.cantidadTotal);

            return total;
        }
        /**
         * GetTotalPeso() - Devuelve el total de peso de todos los libros.
         */

        public void eliminarCarrito()
        {
            Items.Clear();

        }
    }
}

