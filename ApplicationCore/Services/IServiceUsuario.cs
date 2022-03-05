using Infrastructure.Models;

namespace ApplicationCore.Services
{
    public interface IServiceUsuario
    {
        Usuario Get_UsuarioByID(int id);
        Usuario Save(Usuario usuario);
        Usuario Get_Usuario(string email, string password);
    }
}
