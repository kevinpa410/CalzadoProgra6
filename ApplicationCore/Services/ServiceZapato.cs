using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.Repository.Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public  class ServiceZapato : IServiceZapato
    {
        public IEnumerable<Zapato> GetZapato()
        {
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.GetZapato();
            
        }
        public Zapato GetZapatoByID(int id)
        {
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.GetZapatoByID(id);
        }
           
        public IEnumerable<Zapato> GetZapatoByProveedor(int idProveedor)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Zapato> GetZapatoByCategoria(int idCategoria)
        {
            throw new NotImplementedException();
        }

    }
}
