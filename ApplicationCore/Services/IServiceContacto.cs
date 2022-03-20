using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceContacto
    {
        IEnumerable<Contacto> GetContacto();
        Contacto GetContactoByID(int id);
        void DeleteAutor(int id);
        Contacto Save(Contacto contacto);
    }
}
