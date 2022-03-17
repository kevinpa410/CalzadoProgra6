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

    namespace Infraestructure.Repository
    {
        public class RepositoryZapato : IRepositoryZapato
        {
            public IEnumerable<Zapato> GetZapato()
            {
                try
                {

                    IEnumerable<Zapato> lista = null;
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        //Select * from Zapato
                        lista = ctx.Zapato.Include("Proveedor").ToList<Zapato>();
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

            public Zapato GetZapatoByID(int id)
            {
                Zapato oZapato = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oZapato = ctx.Zapato.
                          Where(l => l.idZapato == id).
                          Include(a => a.Proveedor).
                          Include(c => c.Categoria).
                          FirstOrDefault();
               
                    oZapato = ctx.Zapato.Find(id);
                  
                }
                return oZapato;
            }

            public IEnumerable<Zapato> GetZapatoByProveedor(int idProveedor)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Zapato> GetZapatoByCategoria(int idCategoria)
            {
                throw new NotImplementedException();
            }
        }
    }

}
