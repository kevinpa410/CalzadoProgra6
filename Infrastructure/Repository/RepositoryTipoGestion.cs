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
            List<TipoGestion> ordenes = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    ordenes = ctx.TipoGestion.
                               Include("Cliente").
                               ToList<TipoGestion>();

                }
                return ordenes;

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

        public TipoGestion GetTipoGestionByID(int id)
        {
            TipoGestion tipoGestion = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    tipoGestion = ctx.TipoGestion.
                               Include("Cliente").
                               Include("Entradas_Salidas").
                               Include("Entradas_Salidas.Zapato").
                               Where(p => p.IdTipoGestion == id).
                               FirstOrDefault<TipoGestion>();

                }
                return tipoGestion;

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

        public TipoGestion Save(TipoGestion pTipoGestion)
        {
            int resultado = 0;
            TipoGestion tipoGestion = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Orden
                // 2- OrdenDetalle 
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.TipoGestion.Add(pTipoGestion);
                        resultado = ctx.SaveChanges();
                        foreach (var detalle in pTipoGestion.Entradas_Salidas)
                        {
                            detalle.IdTipoGestion = pTipoGestion.IdTipoGestion;
                        }
                        foreach (var item in pTipoGestion.TipoGestion)
                        {
                            // Busco el producto que está en el detalle por IdLibro
                            Zapato oZapato = ctx.Zapato.Find(item.IdZapato);

                            // Se indica que se alteró
                            ctx.Entry(oZapato).State = EntityState.Modified;
                            // Guardar                         
                            resultado = ctx.SaveChanges();
                        }

                        // Commit 
                        transaccion.Commit();
                    }
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    tipoGestion = GetTipoGestionByID(pTipoGestion.IdTipoGestion);


                return tipoGestion;
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

    }
}
}
