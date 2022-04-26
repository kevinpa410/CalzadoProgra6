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
        public Zapato Save(Zapato zapato, string[] selectedProveedor, string[] selectedUbicacion)
        {
            IRepositoryZapato repository = new RepositoryZapato();
            return repository.Save(zapato, selectedProveedor, selectedUbicacion);

        }

    }
}
