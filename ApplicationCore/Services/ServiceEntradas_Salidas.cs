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

        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByProveedor(int idProveedor)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_SalidasByProveedor(idProveedor);
        }
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByZapato(int idZapato)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByUbicacion(string Ubicacion)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_SalidasByUbicacion(Ubicacion);
        }
        public Entradas_Salidas Save(Entradas_Salidas entradas_salidas)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.Save(entradas_salidas);

        }


    }
}