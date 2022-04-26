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
        Zapato Save(Zapato zapato, string[] selectedProveedor, string[] selectedUbicacion);
    }
}
