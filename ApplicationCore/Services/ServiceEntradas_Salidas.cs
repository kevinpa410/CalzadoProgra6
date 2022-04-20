using Infrastructure.Models;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceEntradas_Salidas : IServiceEntradas_Salidas
    {
        public IEnumerable<Entradas_Salidas> GetEntradas_Salidas()
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_Salidas();

        }
        public Entradas_Salidas GetEntradas_SalidasByID(int id)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_SalidasByID(id);
        }

        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByZapato(int idZapato)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.GetEntradas_SalidasByZapato(idZapato);
        }
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByTipoGestion(int idTipoGestion)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Entradas_Salidas> GetEntradas_SalidasByUbicacion(int idUbicacion)
        {
            throw new NotImplementedException();
        }
        public Entradas_Salidas Save(Entradas_Salidas entradas_salidas)
        {
            IRepositoryEntradas_Salidas repository = new RepositoryEntradas_Salidas();
            return repository.Save(entradas_salidas);

        }

    }
}
