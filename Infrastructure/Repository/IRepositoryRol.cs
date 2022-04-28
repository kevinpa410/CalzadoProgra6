﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryRol
    {
        IEnumerable<Rol> GetTipoGestion();
        Rol GetTipoGestionByID(int id);

    }
}
