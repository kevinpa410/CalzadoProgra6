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

            string crytpPasswd = Cryptography.EncrypthAES(password);

            return repository.GetUsuario(email, crytpPasswd);
        }

        public Usuario Get_UsuarioByID(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario Save(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
