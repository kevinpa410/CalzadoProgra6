using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    internal partial class ZapatoMetadata
    {
    }

    internal partial class UsuarioMetadata
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        [DataType(DataType.EmailAddress, ErrorMessage = "{0} no tiene formato válido")]
        public string Email { get; set; }
        public int IdRol { get; set; }

        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public bool Estado { get; set; }
        public virtual Rol Rol { get; set; }

        [Display(Name = "Categoría")]
        public virtual ICollection<Categoria> Categoria { get; set; }

    }

    internal partial class ZapatoMetada
    {

        [Display(Name = "Id Zapatdo")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int idZapato { get; set; }


        public string color { get; set; }


        public string descripcion { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico y con dos decimales")]
        public int cantidadTot { get; set; }

        [Display(Name = "Cantidad Maxima")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico y con dos decimales")]
        public int cantMax { get; set; }

        [Display(Name = "Cantidad Minima")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico y con dos decimales")]
        public int cantMin { get; set; }

        [Display(Name = "Categoría")]
        public virtual ICollection<Categoria> Categoria { get; set; }

    }





}
