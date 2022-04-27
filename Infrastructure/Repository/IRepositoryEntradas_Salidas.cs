using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryEntradas_Salidas
    {
        IEnumerable<Entradas_Salidas> GetEntradas_Salidas();
        Entradas_Salidas GetEntradas_SalidasByID(int id);
        void DeleteEntradas_Salidas(int id);
        Entradas_Salidas Save(Entradas_Salidas entradas_salidas);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByUbicacion(String Ubicacion);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByProveedor(int idProveedor);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByZapato(int idZapato);
      
    }
}
