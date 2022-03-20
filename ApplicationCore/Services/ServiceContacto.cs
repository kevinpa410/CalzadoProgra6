using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceContacto : IServiceContacto
    {        
        public IEnumerable<Contacto> GetContacto()
        {
            IRepositoryContacto repository = new RepositoryContacto();
            return repository.GetContacto();
        }

        public Contacto GetContactoByID(int id)
        {
            RepositoryContacto repository = new RepositoryContacto();
            return repository.GetContactoByID(id);
        }

        public Contacto Save(Contacto contacto)
        {
            IRepositoryContacto repository = new RepositoryContacto();
            return repository.Save(contacto);
        }

        public void DeleteAutor(int id) //Pending Code
        {
            throw new NotImplementedException();
        }
    }
}
