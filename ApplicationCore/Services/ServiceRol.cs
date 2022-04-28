using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public IEnumerable<TipoGestion> GetTipoGestion()
        {
            IRepositoryTipoGestion repository = new RepositoryTipoGestion();
            return repository.GetTipoGestion();
        }

        public TipoGestion GetTipoGestionByID(int id)
        {
            IRepositoryTipoGestion repository = new RepositoryTipoGestion();
            return repository.GetTipoGestionByID(id);
        }
    }
}
