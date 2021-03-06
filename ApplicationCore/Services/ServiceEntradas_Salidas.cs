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

        public IEnumerable<Entradas_Salidas> GetEntradas_Salidas()
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_Salidas();
        }

        public Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_SalidasByID(id);
        }

        public Entradas_Salidas Save(Entradas_Salidas pEntradas_Salidas, string[] selectedUbicacion)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.Save(pEntradas_Salidas, selectedUbicacion);
        }
    }
}
