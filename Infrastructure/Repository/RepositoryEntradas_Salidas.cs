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
            List<Entradas_Salidas> entradas_salidas = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    entradas_salidas = ctx.Entradas_Salidas.
                        Include("Usuario").
                        ToList<Entradas_Salidas>();
                }
                return entradas_salidas;
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

        public Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            Entradas_Salidas entradas_salidas = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    entradas_salidas = ctx.Entradas_Salidas.
                        Include("Usuario").
                        Include("Entradas_Salidas").
                        Include("Entradas_Salidas.Zapato").
                        Where(p => p.idGestion == id).
                        FirstOrDefault<Entradas_Salidas>();
                }
                return entradas_salidas;
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
            throw new Exception();
        }
    }
}

    

