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
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    /* La carga diferida retrasa la carga de datos relacionados,
                     * hasta que lo solicite específicamente.*/
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Proveedor proveedor = new Proveedor()
                    {
                        idProveedor = id
                    };
                    ctx.Entry(proveedor).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
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

        public Proveedor Save(Proveedor proveedor, string[] selectedContactos, string[] selectedZapatos)
        {
            int retorno = 0;
            Proveedor oProveedor = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oProveedor = GetProveedorByID((int)proveedor.idProveedor);

                IRepositoryContacto _RepositoryContacto = new RepositoryContacto();
                IRepositoryZapato _RepositoryZapato = new RepositoryZapato();


                if (oProveedor == null)
                {

                    //Insertar
                    if (selectedContactos != null & selectedZapatos != null)
                    {

                        proveedor.Contacto = new List<Contacto>();
                        proveedor.Zapato = new List<Zapato>();

                        var ContactosAndZapatos =  selectedZapatos.Zip(selectedContactos, (c, z) => new { Contactos = c, Zapatos = z });
                       

                        foreach (var cz in ContactosAndZapatos)
                        {

                            var zapatoToAdd = _RepositoryZapato.GetZapatoByID(int.Parse(cz.Zapatos));
                            var check = ctx.Zapato.Where(x => x.idCategoria == zapatoToAdd.idCategoria).FirstOrDefault();
                            if (check != null)
                            {
                                zapatoToAdd = check;
                                ctx.Zapato.Attach(zapatoToAdd);
                                proveedor.Zapato.Add(zapatoToAdd);
                            }

                            var contactoToAdd = _RepositoryContacto.GetContactoByID(int.Parse(cz.Contactos));

                            ctx.Zapato.Attach(zapatoToAdd); //sin esto, EF intentará crear una categoría
                            ctx.Contacto.Attach(contactoToAdd); //sin esto, EF intentará crear una categoría

                            proveedor.Zapato.Add(zapatoToAdd);
                          proveedor.Contacto.Add(contactoToAdd);
                        }




                        //foreach (var zapato in selectedZapatos)
                        //{

                        //    var zapatoToAdd = _RepositoryZapato.GetZapatoByID(int.Parse(zapato));
                        //    var check = ctx.Zapato.Where(x => x.idCategoria == zapatoToAdd.idCategoria).FirstOrDefault();
                        //    if (check != null)
                        //    {
                        //        zapatoToAdd = check;
                        //        ctx.Zapato.Attach(zapatoToAdd);
                        //        proveedor.Zapato.Add(zapatoToAdd);
                        //    }
                        //}


                        //foreach (var contacto in selectedContactos )
                        //{
                        //    var contactoToAdd = _RepositoryContacto.GetContactoByID(int.Parse(contacto));
                        //    ctx.Contacto.Attach(contactoToAdd); //sin esto, EF intentará crear una categoría

                        //    proveedor.Contacto.Add(contactoToAdd);// asociar a la categoría existente con el libro<===============
                        //}


                    }
                    ctx.Proveedor.Add(proveedor);
                    retorno = ctx.SaveChanges();

                }
                else
                {
                    ctx.Proveedor.Add(proveedor);
                    ctx.Entry(proveedor).State = EntityState.Modified;
                    //retorno = ctx.SaveChanges();

                    //Actualizar Categorias
                    var selectedContactosID = new HashSet<string>(selectedContactos);
                    var selectedZapatosID = new HashSet<string>(selectedZapatos);


                    if (selectedContactos != null & selectedZapatos != null)
                    {
                        ctx.Entry(proveedor).Collection(p => p.Contacto).Load();
                        ctx.Entry(proveedor).Collection(p => p.Zapato).Load();

                        var newContactoForProveedor = ctx.Contacto
                         .Where(x => selectedContactosID.Contains(x.idContacto.ToString())).ToList();
                        proveedor.Contacto = newContactoForProveedor;

                        var newZapatoForProveedor = ctx.Zapato
                         .Where(x => selectedZapatosID.Contains(x.idZapato.ToString())).ToList();
                        proveedor.Zapato = newZapatoForProveedor;


                        ctx.Entry(proveedor).State = EntityState.Modified;
                        
                    }
                }
                if (retorno >= 0)
                    oProveedor = GetProveedorByID((int)proveedor.idProveedor);

                try
                {                    
                    retorno = ctx.SaveChanges();
                    return oProveedor;
                }
                catch (Exception)
                {
                    return oProveedor;
                }


            }
        }            
    }


 }
    


       
        
        
    
