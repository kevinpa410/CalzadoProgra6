using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServicesProveedor
    {
        IEnumerable<Proveedor> GetProveedor();
        Proveedor GetProveedorByID(int id);
        void DeleteProveedor(int id);
        Proveedor Save(Proveedor autor);

    }
}
