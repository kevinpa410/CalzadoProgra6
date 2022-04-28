using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Utils;

namespace Infrastructure.Repository
{
    public class RepositoryUbicacion : IRepositoryUbicacion
    {
        public IEnumerable<Ubicacion> GetUbicacion()
        {
            try
            {

                IEnumerable<Ubicacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Ubicacion.ToList<Ubicacion>();
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

        public Ubicacion GetUbicacionByID(int id)
        {
            Ubicacion oUbicacion = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oUbicacion = ctx.Ubicacion.
                      Where(l => l. idUbicacion == id).FirstOrDefault();
                oUbicacion = ctx.Ubicacion.Find(id);
            }
            return oUbicacion;
        }
    }
}
