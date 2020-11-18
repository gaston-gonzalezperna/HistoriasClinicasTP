using HistoriasClinicas.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Data
{
    public class InitializationService : IInitializationService
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _rolmgr;

        public InitializationService(UserManager<Usuario> userManager, RoleManager<Rol> rolmgr)
        {
            _userManager = userManager;
            _rolmgr = rolmgr;
        }

        public async Task SeedAsync()
        {
            iniciarRoles();

            if (_userManager.FindByEmailAsync("admin@madicus.com").Result == null)
            {
                Usuario usuario = new Usuario();
                usuario.Nombre = "Administrador";
                usuario.Apellido = "Administrador";
                usuario.UserName = "admin@madicus.com";
                usuario.NormalizedUserName = usuario.UserName.ToUpper();
                usuario.Email = usuario.UserName;
                usuario.NormalizedEmail = usuario.Email.ToUpper();

                var resultadoDeCreacion = await _userManager.CreateAsync(usuario, "administrador");

                if (resultadoDeCreacion.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usuario, "Administrador");
                }
            }
        }

        private void iniciarRoles()
        {
            _rolmgr.CreateAsync(new Rol() { Name = "Paciente" });
            _rolmgr.CreateAsync(new Rol() { Name = "Empleado" });
            _rolmgr.CreateAsync(new Rol() { Name = "Medico" });
            _rolmgr.CreateAsync(new Rol() { Name = "Administrador" });
        }
    }
}
