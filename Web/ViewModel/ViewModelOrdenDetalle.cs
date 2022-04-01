using ApplicationCore.Services;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ViewModelOrdenDetalle
    {

        public int idEntrada_Salida { get; set; }
        public int idZapato { get; set; }
        public int cantidadTotal{ get; set; }
        public int idUsuario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idGestion { get; set; }



        public virtual Zapato Zapato { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual TipoGestion TipoGestion { get; set; }


        public ViewModelOrdenDetalle(int idZapato)
        {
            IServiceZapato _ServiceZapato = new ServiceZapato();
            this.idZapato = idZapato;
            this.Zapato = _ServiceZapato.GetZapatoByID(idZapato);
        }
        
    }
}