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
        public void DeleteProveedor(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    /* La carga diferida retrasa la carga de datos relacionados,
                     * hasta que lo solicite específicamente.*/
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Proveedor proveedor = new Proveedor()
                    {
                        idProveedor = id
                    };
                    ctx.Entry(proveedor).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
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

        public IEnumerable<Proveedor> GetProveedor()
        {
            try
            {

                IEnumerable<Proveedor> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Zapato
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
                      Include(a => a.nombre).
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
            try
            {

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
                    oProveedor = GetProveedorByID((int)proveedor.idProveedor);
                return oProveedor;

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
    }
}
