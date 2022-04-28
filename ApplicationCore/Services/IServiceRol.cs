﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceRol
    {
        IEnumerable<Rol> GetRol();
        Rol GetRolByID(int id);


    }
}
