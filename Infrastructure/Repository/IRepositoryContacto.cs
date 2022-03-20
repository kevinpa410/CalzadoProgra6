using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryContacto
    {
        IEnumerable<Contacto> GetContacto();
        Contacto GetContactoByID(int id);
        void DeleteContacto(int id);
        Contacto Save(Contacto contacto);

    }
}
