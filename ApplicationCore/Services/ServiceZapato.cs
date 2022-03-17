using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class ServiceZapato : IServiceZapato
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
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.GetZapatoByProveedor(idProveedor);
        }
        public IEnumerable<Zapato> GetZapatoByCategoria(int idCategoria)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Zapato> GetZapatoByUbicacion(string ubicacion)
        {
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.GetZapatoByUbicacion(ubicacion);
        }
        public Zapato Save(Zapato zapato)
        {
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.Save(zapato);

        }

    }
}
