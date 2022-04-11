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
            int resultado = 0;
            Entradas_Salidas movimiento = null;
            try
            {
                // Salvar pero con transacción porque son 2 tablas
                // 1- Orden
                // 2- OrdenDetalle 
                using (MyContext ctx = new MyContext())
                {
                    //using (var transaccion = ctx.Database.BeginTransaction())
                    //{
                    //    ctx.Entradas_Salidas.Add(pEntradas_Salidas);
                    //    resultado = ctx.SaveChanges();
                    //    foreach (var detalle in pEntradas_Salidas.OrdenDetalle)
                    //    {
                    //        detalle.IdOrden = pEntradas_Salidas.IdOrden;
                    //    }
                    //    foreach (var item in pEntradas_Salidas.OrdenDetalle)
                    //    {
                    //        // Busco el producto que está en el detalle por IdLibro
                    //        Libro oLibro = ctx.Libro.Find(item.IdLibro);
                            
                    //        // Se indica que se alteró
                    //        ctx.Entry(oLibro).State = EntityState.Modified;
                    //        // Guardar                         
                    //        resultado = ctx.SaveChanges();
                    //    }
                    //    // Commit 
                    //    transaccion.Commit();
                    //}
                }

                // Buscar la orden que se salvó y reenviarla
                if (resultado >= 0)
                    movimiento = GetEntradas_SalidasByID(pEntradas_Salidas.idEntradas_Salidas);


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
    }
}

    

