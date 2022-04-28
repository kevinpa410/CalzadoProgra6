using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public class ServiceRol : IServiceRol
    {
        public IEnumerable<Rol> GetRol()
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRol();
        }

        public Rol GetRolByID(int id)
        {
            IRepositoryRol repository = new RepositoryRol();
            return repository.GetRolByID(id);
        }
    }
}
