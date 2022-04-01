using Infrastructure.Models;
using System.Collections.Generic;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario Get_UsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario Get_Usuario(string email, string password);
        IEnumerable<Usuario> Get_Usuario();
    }
}
