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

    namespace Infraestructure.Repository
    {
        public class RepositoryZapato : IRepositoryZapato
        {
            public IEnumerable<Zapato> GetZapato()
            {
                try
                {

                    IEnumerable<Zapato> lista = null;
                    using (MyContext ctx = new MyContext())
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        //Select * from Zapato
                        lista = ctx.Zapato.Include("Proveedor").ToList<Zapato>();
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
                    //*** Sintaxis LINQ Query *** https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-linq-query-operations
                    //var infoZapato = from a in ctxZapato
                    //                where a.IdZapato == id
                    //                select a;
                    //oAutor = infoZapato.First();
                    //*** Sintaxis LINQ Method *** https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/queries-in-linq-to-entities
                    //* Método Find sin ningún otro método, formato automático
                    oZapato = ctx.Zapato.Find(id);
                    //* Con las demás partes de una consulta y se debe dar formato
                    //oAutor = ctx.Zapato.Where(x => x.IdZapato == id).First<Zapato>();
                    //La expresión lambda es una forma más corta de representar un método anónimo utilizando una sintaxis especial.
                }
                return oZapato;
            }
        }
    }

}
