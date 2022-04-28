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
                    entradas_salidas = ctx.Entradas_Salidas.ToList<Entradas_Salidas>();
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
            try
            {
                Entradas_Salidas oEntradas_salidas = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    oEntradas_salidas = ctx.Entradas_Salidas.
                        Where(i => i.idEntradas_Salidas == id).
                        Include(u => u.Usuario).
                        Include(tg => tg.TipoGestion).
                        FirstOrDefault();

                    oEntradas_salidas = ctx.Entradas_Salidas.Find(id);
                }
                return oEntradas_salidas;
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

        ///<==========================================================>\\\ 

        public Entradas_Salidas Save(Entradas_Salidas entradas_Salidas, string[] selectedUbicacion)
        {
            int retorno = 0;
            Entradas_Salidas oEntradas_Salidas = null;

            using (MyContext ctx = new MyContext())
            {
                ctx.Configuration.LazyLoadingEnabled = false;
                oEntradas_Salidas = GetEntradas_SalidasByID((int)entradas_Salidas.idEntradas_Salidas);

                IRepositoryUbicacion _RepositoryUbicacion = new RepositoryUbicacion();
                IRepositoryZapato _RepositoryZapato = new RepositoryZapato();
                IRepositoryUsuario _RepositoryUsuario = new RepositoryUsuario();


                Zapato zapatoMovimiento = _RepositoryZapato.GetZapatoByID((int)entradas_Salidas.idZapato);
                Usuario usuarioMovimiento = _RepositoryUsuario.GetUsuarioByID((int)entradas_Salidas.idUsuario);


                if (oEntradas_Salidas == null)
                {
                    if (selectedUbicacion != null)
                    {
                        zapatoMovimiento.Ubicacion = new List<Ubicacion>();
                        

                        foreach (var ubicacion in selectedUbicacion)
                        {
                            var checkZapato = ctx.Zapato.Where(x => x.idCategoria == zapatoMovimiento.idCategoria).FirstOrDefault();

                            if (checkZapato != null)
                            {
                                zapatoMovimiento = checkZapato;
                                var UbicacionToAdd = _RepositoryUbicacion.GetUbicacionByID(int.Parse(ubicacion));
                                ctx.Ubicacion.Attach(UbicacionToAdd);
                                zapatoMovimiento.Ubicacion.Add(UbicacionToAdd);

                            }
                        }


                        var checkUsuario = ctx.Usuario.Where(x => x.idUsuario == entradas_Salidas.idUsuario).FirstOrDefault();
                        if (checkUsuario != null)
                        {
                            usuarioMovimiento = checkUsuario;

                            ctx.Usuario.Attach(usuarioMovimiento);
                        }

                    }

                    entradas_Salidas.Zapato = zapatoMovimiento;
                    entradas_Salidas.Usuario = usuarioMovimiento;

                    ctx.Entradas_Salidas.Add(entradas_Salidas);
                    retorno = ctx.SaveChanges();
                }
                else
                {
                    ctx.Entradas_Salidas.Add(entradas_Salidas);
                    ctx.Entry(entradas_Salidas).State = EntityState.Modified;
                    retorno = ctx.SaveChanges();

                    var selectedUbicacionID = new HashSet<string>(selectedUbicacion);


                    if ( selectedUbicacion != null)
                    {

                        ctx.Entry(entradas_Salidas).Collection(p => p.Zapato. Ubicacion).Load();

                        var newProveedorForZapato = ctx.Ubicacion
                         .Where(x => selectedUbicacionID.Contains(x.idUbicacion.ToString())).ToList();
                        entradas_Salidas.Zapato.Ubicacion = newProveedorForZapato;

                        ctx.Entry(entradas_Salidas).State = EntityState.Modified;

                    }
                }
                if (retorno >= 0)
                    oEntradas_Salidas = GetEntradas_SalidasByID((int)entradas_Salidas.idEntradas_Salidas);

                try
                {
                    retorno = ctx.SaveChanges();
                    return oEntradas_Salidas;
                }
                catch (Exception)
                {
                    return oEntradas_Salidas;
                }

            }
        }
    }
}

    

