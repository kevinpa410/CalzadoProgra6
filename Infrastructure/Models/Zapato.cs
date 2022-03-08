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
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ZapatoMetada))]
    public partial class Zapato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zapato()
        {
            this.Entradas_Salidas = new HashSet<Entradas_Salidas>();
            this.Proveedor = new HashSet<Proveedor>();
            this.Ubicacion = new HashSet<Ubicacion>();
        }
    
        public int idZapato { get; set; }
        public int idCategoria { get; set; }
        public string color { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> cantMax { get; set; }
        public Nullable<int> cantMin { get; set; }
        public Nullable<int> cantidadTot { get; set; }
    
        public virtual Categoria Categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entradas_Salidas> Entradas_Salidas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proveedor> Proveedor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ubicacion> Ubicacion { get; set; }
    }
}