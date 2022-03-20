using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryProveedor : IRepositoryProveedor
    {
        public void DeleteProveedor(int id) //Pending Code
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            try
            {

                IEnumerable<Proveedor> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Proveedor.ToList<Proveedor>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Proveedor GetProveedorByID(int id)
        {
            Proveedor oProveedor = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = ctx.Proveedor.
                      Where(l => l.idProveedor == id).
                      Include(a => a.Zapato).
                      Include(c => c.Contacto).
                      FirstOrDefault();

                oProveedor = ctx.Proveedor.Find(id);

            }
            return oProveedor;
        }

        public Proveedor Save(Proveedor proveedor)
        {
            int retorno = 0;
            Proveedor oProveedor = null;

            using (MyContext ctx = new MyContext())
            {

                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = GetProveedorByID((int)proveedor.idProveedor);
                if (oProveedor == null)
                {
                    ctx.Proveedor.Add(proveedor);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Proveedor.Add(proveedor);
                    ctx.Entry(proveedor).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }



            }
            if (retorno >= 0)
                oProveedor = GetProveedorByID(proveedor.idProveedor);

            return oProveedor;
        }
           
    }

}
    


       
        
        
    
