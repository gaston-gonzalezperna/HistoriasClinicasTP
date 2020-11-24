using HistoriasClinicas2.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Data
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

            public void Seed()
            {
                IniciarRoles();

                if (_userManager.FindByEmailAsync("admin@madicus.com").Result == null)
                {
                    Usuario usuario = new Usuario();
                    usuario.UserName = "admin@madicus.com";
                    usuario.NormalizedUserName = usuario.UserName.ToUpper();
                    usuario.Email = usuario.UserName;
                    usuario.NormalizedEmail = usuario.Email.ToUpper();

                    var resultadoDeCreacion = _userManager.CreateAsync(usuario, "administrador").Result;

                    if (resultadoDeCreacion.Succeeded)
                    {
                        _userManager.AddToRoleAsync(usuario, "Administrador").Wait();
                    }
                }
            }

            private void IniciarRoles()
            {
                _rolmgr.CreateAsync(new Rol() { Name = "Paciente" }).Wait();
                _rolmgr.CreateAsync(new Rol() { Name = "Empleado" }).Wait();
                _rolmgr.CreateAsync(new Rol() { Name = "Medico" }).Wait();
                _rolmgr.CreateAsync(new Rol() { Name = "Administrador" }).Wait();
            }
        }
    }
