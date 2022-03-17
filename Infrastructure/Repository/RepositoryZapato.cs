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

    public class RepositoryZapato : IRepositoryZapato
    {
        public void DeleteZapato(int idZapato)
        {
            throw new NotImplementedException();
        }
    
        public IEnumerable<Zapato> GetZapato()
        {
            try
            {

                IEnumerable<Zapato> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Select * from Zapato
                    lista = ctx.Zapato.Include(x => x.Proveedor).ToList<Zapato>();
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
        public IEnumerable<Zapato> GetZapatoByUbicacion(string ubicacion)
        {
            throw new NotImplementedException();
        }
        //{
        // IEnumerable<Zapato> lista = null;
        //using (MyContext ctx = new MyContext())
        // {
        // ctx.Configuration.LazyLoadingEnabled = false;
        //lista = ctx.Zapato.ToList().
        //FindAll(l => l.Ubicacion.ToLower().Contains(Ubicacion.ToLower()));
        // }
        // }
        public Zapato Save(Zapato zapato) //string[] selectedCategorias
        {
            int retorno = 0;
            Zapato oZapato = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oZapato = GetZapatoByID((int)zapato.idZapato);
                IRepositoryCategoria _RepositoryCategoria = new RepositoryCategoria();
                if (oZapato == null)
                {
                    ctx.Zapato.Add(zapato);
                    retorno = ctx.SaveChanges();
                    

                }
                else
                {
                    ctx.Zapato.Add(zapato);
                    ctx.Entry(zapato).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                }
            }
            if (retorno >= 0)
                oZapato = GetZapatoByID((int)zapato.idZapato);
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


