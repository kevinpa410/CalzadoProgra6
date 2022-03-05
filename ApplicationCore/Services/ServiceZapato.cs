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
            throw new NotImplementedException();
        }
           


    }
}
