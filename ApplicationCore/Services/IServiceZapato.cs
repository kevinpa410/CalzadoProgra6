using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceZapato
    {
        IEnumerable<Zapato> GetZapato();
        Zapato GetZapatoByID(int id);
        IEnumerable<Zapato> GetZapatoByUbicacion(String ubicacion);
        IEnumerable<Zapato> GetZapatoByProveedor(int idProveedor);
        IEnumerable<Zapato> GetZapatoByCategoria(int idCategoria);
        Zapato Save(Zapato zapato);
    }
}
