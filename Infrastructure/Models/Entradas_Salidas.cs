//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Entradas_Salidas
    {
        public int idEntradas_Salidas { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public Nullable<int> idGestion { get; set; }
        public int idZapato { get; set; }
        public Nullable<int> cantidadTotal { get; set; }
        public int idUsuario { get; set; }
    
        public virtual TipoGestion TipoGestion { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Zapato Zapato { get; set; }
    }
}