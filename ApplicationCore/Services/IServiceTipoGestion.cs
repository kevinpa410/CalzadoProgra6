﻿using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    interface IServiceTipoGestion
    {
        IEnumerable<TipoGestion> GetTipoGestion();
        IEnumerable<TipoGestion> GetTipoGestionByProveedor(string proveedor);
        IEnumerable<TipoGestion> GetTipoGestionByZapato(int idZapato);
        TipoGestion GetTipoGestionByID(int id);
        void DeleteTipoGestion(int id);
        TipoGestion Save(TipoGestion TipoGestionn, string[] selectedProveedores);
    }
}
