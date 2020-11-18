using HistoriasClinicas.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Data
{
    public class Seeder
    {
        public static void Inicializar(UserManager<Usuario> usrmgr)
        {
            if (usrmgr.FindByEmailAsync("admin@madicus.com").Result == null)
            {
                Usuario admin = new Usuario();
                admin.Nombre = "Administrador";
                admin.Apellido = "";
                admin.DNI = "";
                admin.PhoneNumber = "";
                admin.Direccion = "";
                admin.Email = "admin@madicus.com";
                admin.NormalizedEmail = admin.Email.ToUpper();
                admin.UserName = admin.Email;

                IdentityResult result = usrmgr.CreateAsync(admin, "Administrador").Result;

                if (result.Succeeded)
                {
                    usrmgr.AddToRoleAsync(admin, "Administrador").Wait();
                }
            }
        }
    }
}
