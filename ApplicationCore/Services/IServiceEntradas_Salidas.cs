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

        IEnumerable<Gestion_Entradas_Salidas> GetEntradas_Salidas();
        Gestion_Entradas_Salidas GetEntradas_SalidasByID(int id);
        void DeleteEntradas_Salidas(int id);
        Gestion_Entradas_Salidas Save(Gestion_Entradas_Salidas Entradas_Salidas);
    }
}
