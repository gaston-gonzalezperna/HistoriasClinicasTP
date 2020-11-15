using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas.Data;
using HistoriasClinicas.Models;
using Microsoft.AspNetCore.Identity;

namespace HistoriasClinicas.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<Rol> _rolmgr;

        public RolesController(RoleManager<Rol> rolmgr)
        {
            this._rolmgr = rolmgr;
        }

        public async Task<IActionResult> CrearRoles()
        {
            Rol ir1 = new Rol() { Name = "Paciente" };
            Rol ir2 = new Rol() { Name = "Empleado" };
            Rol ir3 = new Rol() { Name = "Medico" };
            Rol ir4 = new Rol() { Name = "Administrador" };

            var resultado1 = await _rolmgr.CreateAsync(ir1);
            var resultado2 = await _rolmgr.CreateAsync(ir2);
            var resultado3 = await _rolmgr.CreateAsync(ir3);
            var resultado4 = await _rolmgr.CreateAsync(ir4);

            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            ViewBag.Roles = _rolmgr.Roles.ToList();
            return View();
        }
    }
}