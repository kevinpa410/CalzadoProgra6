using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ApplicationCore.Services
{
    public class ServicesProveedor : IServicesProveedor
    {
        public void DeleteProveedor(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedor();
        }

        public Proveedor GetProveedorByID(int id)
        {
            throw new NotImplementedException();
        }

        public Proveedor Save(Proveedor autor)
        {
            throw new NotImplementedException();
        }
    }
}
