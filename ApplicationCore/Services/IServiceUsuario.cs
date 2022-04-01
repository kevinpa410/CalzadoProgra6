using Infrastructure.Models;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuarioByID(int id);
        Usuario Save(Usuario usuario);
<<<<<<< HEAD
        Usuario Get_Usuario(string email, string password);
        IEnumerable<Usuario> Get_Usuario();
=======
        Usuario GetUsuario(string email, string password);
>>>>>>> 44aea95909f7a482f2b128d8d88c6a9e101e24fc
    }
}
