
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
    public class RepositoryEntradas_Salidas :IRepositoryEntradas_Salidas
    {
        public void DeleteEntradas_Salidas(int idEntradas_Salidas)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entradas_Salidas> GetEntradas_Salidas()
        {
            try
            {

                IEnumerable<Entradas_Salidas> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Zapato
                    lista = ctx.Entradas_Salidas.Include(x => x.Zapato).ToList<Entradas_Salidas>();
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

        public Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            Entradas_Salidas oEntradas_Salidas = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oEntradas_Salidas = ctx.Entradas_Salidas.
                      Where(l => l.idEntradas_Salidas == id).
                      Include(a => a.Zapato).
                       Include(c => c.Proveedor).
                      FirstOrDefault();

                oEntradas_Salidas = ctx.Entradas_Salidas.Find(id);

            }
            return oEntradas_Salidas;
        }
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByUbicacion(string Ubicacion)
        {
        throw new NotImplementedException();
         }
        public Entradas_Salidas Save(Entradas_Salidas entradas_salidas) //string[] selectedCategorias
        {
            int retorno = 0;
            Entradas_Salidas oEntradas_Salidas = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oEntradas_Salidas = GetEntradas_SalidasByID((int)entradas_salidas.idEntradas_Salidas);
                IRepositoryZapato _RepositoryZapato = new RepositoryZapato();
                if (oEntradas_Salidas == null)
                {
                    ctx.Entradas_Salidas.Add(entradas_salidas);
                    retorno = ctx.SaveChanges();
                    

                }
                else
                {
                    ctx.Entradas_Salidas.Add(entradas_salidas);
                    ctx.Entry(entradas_salidas).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                }
            }
            if (retorno >= 0)
                oEntradas_Salidas = GetEntradas_SalidasByID((int)entradas_salidas.idEntradas_Salidas);
            return oEntradas_Salidas;
        }

        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByZapato(int idZapato)
        {
            throw new NotImplementedException();
        }
       
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByProveedor(int idProveedor)
        {
            throw new NotImplementedException();

        }

    }

}

    

