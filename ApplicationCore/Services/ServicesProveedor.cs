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
            IRepositoryProveedor repositoryProveedor = new RepositoryProveedor();
            repositoryProveedor.DeleteProveedor(id);
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedor();
        }

        public Proveedor GetProveedorByID(int id)
        {
            IRepositoryProveedor repository = new RepositoryProveedor();
            return repository.GetProveedorByID(id);
        }

        public Proveedor Save(Proveedor proveedor, string[] selectedContactos, string[] selectedZapatos)
        {
            IRepositoryProveedor repositoryProveedor = new RepositoryProveedor();
            return repositoryProveedor.Save(proveedor, selectedContactos, selectedZapatos);
        }

        
    }
}
