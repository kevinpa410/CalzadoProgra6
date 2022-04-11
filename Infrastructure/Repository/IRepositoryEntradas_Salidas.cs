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
        Entradas_Salidas Save(Entradas_Salidas pEntradas_Salidas);
    }
}
