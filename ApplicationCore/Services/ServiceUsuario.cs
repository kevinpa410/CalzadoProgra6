using Infrastructure.Models;
using Infrastructure.Repository;
using ApplicationCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ServiceUsuario : IServiceUsuario
    {
        public Usuario Get_Usuario(string email, string password)
        {

            IRepositoryUsuario repository = new RepositoryUsuario();
            return repository.GetUsuario(email, password);
        }

        public IEnumerable<Usuario> Get_Usuario()
        {
            throw new NotImplementedException();
        }

        public Usuario Get_UsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.password = oUsuario.password;

            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.password = usuario.password;

            return repository.Save(usuario);
        }
    }
}