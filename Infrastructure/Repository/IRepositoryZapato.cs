using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryZapato
    {
        IEnumerable<Zapato> GetZapato();
        Zapato GetZapatoByID(int id);
        void DeleteZapato(int id);
        Zapato Save(Zapato zapato, string[] selectedProveedor, string[] selectedUbicacion); //string[] selectedCategorias
        IEnumerable<Zapato> GetZapatoByUbicacion(String ubicacion);
        IEnumerable<Zapato> GetZapatoByProveedor(int idProveedor);
        IEnumerable<Zapato> GetZapatoByCategoria(int idCategoria);
    }
}
