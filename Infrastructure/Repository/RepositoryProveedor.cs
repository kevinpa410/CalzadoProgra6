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
    public class RepositoryProveedor : IRepositoryProveedor
    {
        public void DeleteProveedor(int id) //Pending Code
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Proveedor> GetProveedor()
        {
            try
            {

                IEnumerable<Proveedor> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    lista = ctx.Proveedor.ToList<Proveedor>();
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

        public Proveedor GetProveedorByID(int id)
        {
            Proveedor oProveedor = null;
            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = ctx.Proveedor.
                      Where(l => l.idProveedor == id).
                      Include(a => a.Zapato).
                      Include(c => c.Contacto).
                      FirstOrDefault();

                oProveedor = ctx.Proveedor.Find(id);

            }
            return oProveedor;
        }

        public Proveedor Save(Proveedor proveedor, string[] selectedContactos)
        {
            int retorno = 0;
            Proveedor oProveedor = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = GetProveedorByID((int)proveedor.idProveedor);
                IRepositoryContacto _RepositoryCategoria = new RepositoryContacto();
                IRepositoryZapato _RepositoryZapato = new RepositoryZapato();

                if (oProveedor == null)
                {

                    //Insertar
                    if (selectedContactos != null)
                    {

                        proveedor.Contacto = new List<Contacto>();
                        foreach (var contacto in selectedContactos)
                        {
                            var contactoToAdd = _RepositoryCategoria.GetContactoByID(int.Parse(contacto));
                            ctx.Contacto.Attach(contactoToAdd); //sin esto, EF intentará crear una categoría
                            proveedor.Contacto.Add(contactoToAdd);// asociar a la categoría existente con el libro


                        }
                    }
                    ctx.Proveedor.Add(proveedor);
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
                    ctx.Proveedor.Add(proveedor);
                    ctx.Entry(proveedor).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();
                    //Actualizar Categorias
                    var selectedContactosID = new HashSet<string>(selectedContactos);
                    if (selectedContactos != null)
                    {
                        ctx.Entry(proveedor).Collection(p => p.Contacto).Load();
                        var newCategoriaForLibro = ctx.Contacto
                         .Where(x => selectedContactosID.Contains(x.idContacto.ToString())).ToList();
                        proveedor.Contacto = newCategoriaForLibro;

                        ctx.Entry(proveedor).State = EntityState.Modified;
                        retorno = ctx.SaveChanges();
                    }
                }
            }




            if (retorno >= 0)oProveedor = GetProveedorByID((int)proveedor.idProveedor);

            return oProveedor;
        }


    }

}
    


       
        
        
    
