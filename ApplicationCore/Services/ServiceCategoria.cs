﻿using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceCategoria : IServiceCategoria
    {
        public IEnumerable<Categoria> GetCategoria()
        {
            IRepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategoria();
        }

        public Categoria GetCategoriaByID(int id)
        {
            RepositoryCategoria repository = new RepositoryCategoria();
            return repository.GetCategoriaByID(id);
        }


    }
}
