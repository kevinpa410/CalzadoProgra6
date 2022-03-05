using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEntradas_Salidas : IServiceEntradas_Salidas
    {
        public void DeleteEntradas_Salidas(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gestion_Entradas_Salidas> GetEntradas_Salidas()
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_Salidas();
        }

        public Gestion_Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            throw new NotImplementedException();
        }

        public Gestion_Entradas_Salidas Save(Gestion_Entradas_Salidas Entradas_Salidas)
        {
            throw new NotImplementedException();
        }
    }
}
