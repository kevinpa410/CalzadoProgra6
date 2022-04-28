using Infrastructure.Models;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario GetUsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario GetUsuario(string email, string password);
        IEnumerable<Usuario> GetUsuarios();
    }
}
