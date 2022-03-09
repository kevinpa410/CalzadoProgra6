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



}
