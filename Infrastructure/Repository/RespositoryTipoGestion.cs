using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    class RespositoryTipoGestion
    {
        public void DeleteTipoGestion(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TipoGestion> GetTipoGestion()
        {
            IEnumerable<TipoGestion> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoGestion.Include(x => x.Proveedores).ToList();
            }
            return lista;
        }

        public IEnumerable<TipoGestion> GetGestionByZapato(int idZapato)
        {
            IEnumerable<TipoGestion> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoGestion.Where(l => l.idProveedores == idZapato).ToList();
            }
            return lista;
        }



        public TipoGestion GetTipoGestionByID(int id)
        {
            TipoGestion otipoGestion = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                otipoGestion = ctx.TipoGestion.
                    Where(l => l.idTipoGestion == id).
                     Include(x => x.Proveedores).Include(x => x.Zapato)
                            .FirstOrDefault();
            }
            return otipoGestion;
        }

        public IEnumerable<TipoGestion> GetTipoGestionByProveedores(string proveedores)
        {
            IEnumerable<TipoGestion> lista = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                lista = ctx.TipoGestion.ToList().
                    FindAll(l => l.Proveedores.ToLower().Contains(proveedores.ToLower()));
            }
            return lista;
        }

        public TipoGestion Save(TipoGestion tipoGestion, string[] selectedProveedores)
        {
            int retorno = 0;
            TipoGestion oTipoGestion = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oTipoGestion = GetTipoGestionByID((int)tipoGestion.idTipoGestion);
                IRepositoryProveedores _RepositoryProveedores = new RepositoryProveedores();

                if (oTipoGestion == null)
                {

                    //Insertar
                    if (selectedProveedores != null)
                    {

                        tipoGestion.Proveedores = new List<Proveedores>();
                        foreach (var proveedores in selectedProveedores)
                        {
                            var ProveedoresToAdd = _RepositoryProveedores.GetProveedoresByID(int.Parse(proveedores));
                            ctx.Proveedores.Attach(ProveedoresToAdd); //sin esto, EF intentará crear una categoría
                            tipoGestion.Proveedores.Add(proveedoresToAdd);// asociar a la categoría existente con el libro


                        }
                    }
                    ctx.TipoGestion.Add(tipoGestion);
                    //SaveChanges
                    //guarda todos los cambios realizados en el contexto de la base de datos.
                    retorno = ctx.SaveChanges();
                    //retorna número de filas afectadas
                }
                else
                {
                    //Registradas: 1,2,3
                    //Actualizar: 1,3,4

                    //Actualizar Libro
                    ctx.TipoGestion.Add(tipoGestion);
                    ctx.Entry(tipoGestion).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //Actualizar Categorias
                    var selectedselectedProveedoresID = new HashSet<string>(selectedProveedores);
                    if (selectedProveedores != null)
                    {
                        ctx.Entry(tipoGestion).Collection(p => p.Proveedores).Load();
                        var newProveedoresForTipoGestion = ctx.Proveedores
                         .Where(x => selectedselectedProveedoresID.Contains(x.idProveedores.ToString())).ToList();
                        tipoGestion.Proveedores = newProveedoresForTipoGestion;

                        ctx.Entry(tipoGestion).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }

            if (retorno >= 0)
                oTipoGestion = GetProveedoresByID((int)tipoGestion.idTipoGestion);

            return oTipoGestion;
        }
    }


}
    }
}
