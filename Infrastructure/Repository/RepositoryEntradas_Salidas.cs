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
        public IEnumerable<Entradas_Salidas> GetEntradas_Salidas()
        {
            List<Entradas_Salidas> movimiento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimiento = ctx.Entradas_Salidas.
                               ToList<Entradas_Salidas>();

                }
                return movimiento;

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
                throw new Exception(mensaje);
            }
        }

        public Entradas_Salidas GetEntradas_SalidasByID(int id)
        {

            Entradas_Salidas movimiento = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    movimiento = ctx.Entradas_Salidas.
                               Include("Usuario").
                               Include("TipoGestion").
                               Where(p => p.idEntradas_Salidas == id).
                               FirstOrDefault<Entradas_Salidas>();

                }
                return movimiento;

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
                throw new Exception(mensaje);
            }
        }

        public Entradas_Salidas Save(Entradas_Salidas pEntradas_Salidas)
        {
            return null;
        }
    }
}

    

