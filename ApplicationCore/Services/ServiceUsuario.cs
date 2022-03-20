﻿using Infrastructure.Models;
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