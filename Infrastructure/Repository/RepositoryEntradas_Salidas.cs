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
    public class RepositoryEntradas_Salidas : IRepositoryEntradas_Salidas
    {
        public void DeleteEntradas_Salidas(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Gestion_Entradas_Salidas> GetEntradas_Salidas()
        {
            try
            {
                IEnumerable<Gestion_Entradas_Salidas> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Gestion_Entradas_Salidas.ToList<Gestion_Entradas_Salidas>();
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

        public Gestion_Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            throw new NotImplementedException();
        }

        public Gestion_Entradas_Salidas Save(Gestion_Entradas_Salidas Entradas_Salidas)
        {
            throw new NotImplementedException();
        }
    }
}
