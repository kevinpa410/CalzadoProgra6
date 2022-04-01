﻿using Infrastructure.Models;
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
            int resultado = 0;
            Entradas_Salidas entradas_salidas = null;
            try
            {
                using (MyContext ctx = new MyContext())
                {
                    using (var transaccion = ctx.Database.BeginTransaction())
                    {
                        ctx.Entradas_Salidas.Add(pEntradas_Salidas);
                        resultado = ctx.SaveChanges();
                        foreach (var detalle in  GetEntradas_Salidas())
                        {
                            detalle.idEntradas_Salidas = pEntradas_Salidas.idEntradas_Salidas;

                        }
                        foreach (var item in GetEntradas_Salidas())
                        {
                            Zapato oZapato = ctx.Zapato.Find(item.idZapato);
                            ctx.Entry(oZapato).State = EntityState.Modified;
                            resultado = ctx.SaveChanges();

                        }
                        transaccion.Commit();
                    }

                }
                if (resultado >= 0)
                    entradas_salidas = GetEntradas_SalidasByID(pEntradas_Salidas.idEntradas_Salidas);

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
    }
}

    

