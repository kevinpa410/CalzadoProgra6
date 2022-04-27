using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IServiceEntradas_Salidas
    {
        IEnumerable<Entradas_Salidas> GetEntradas_Salidas();
        Entradas_Salidas GetEntradas_SalidasByID(int id);
      
        Entradas_Salidas Save(Entradas_Salidas entradas_salidas);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByUbicacion(String Ubicacion);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByZapato(int idZapato);
        IEnumerable<Entradas_Salidas> GetEntradas_SalidasByProveedor(int idProveedor);
        

    }
}
