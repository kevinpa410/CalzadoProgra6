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
    public class RepositoryTipoGestion : IRepositoryTipoGestion
    {
        public IEnumerable<TipoGestion> GetTipoGestion()
        {
            try
            {

                IEnumerable<TipoGestion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Zapato
                    lista = ctx.TipoGestion.ToList<TipoGestion>();
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

        public TipoGestion GetTipoGestionByID(int id)
        {
            TipoGestion oTipoGestion = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oTipoGestion = ctx.TipoGestion.FirstOrDefault();

                oTipoGestion = ctx.TipoGestion.Find(id);

            }
            return oTipoGestion;
        }
    }
}
