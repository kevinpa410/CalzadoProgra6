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
        public Usuario GetUsuario(string email, string password)
        {

            IRepositoryUsuario repository = new RepositoryUsuario();

            // Encriptar el password para poder compararlo
            //string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.GetUsuario(email, password);
        }

        public Usuario GetUsuarioByID(int id)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            Usuario oUsuario = repository.GetUsuarioByID(id);
            oUsuario.password = Cryptography.DecrypthAES(oUsuario.password);
            return oUsuario;
        }

        public Usuario Save(Usuario usuario)
        {
            IRepositoryUsuario repository = new RepositoryUsuario();
            usuario.password = Cryptography.EncrypthAES(usuario.password);
            return repository.Save(usuario);
        }
    }
}