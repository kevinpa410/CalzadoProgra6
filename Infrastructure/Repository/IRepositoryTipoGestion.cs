using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public interface IRepositoryTipoGestion
    {
        TipoGestion GetTipoGestionByID(int id);
        IEnumerable<TipoGestion> GetTipoGestion();
        TipoGestion Save(TipoGestion tipoGestion);
    }
}
