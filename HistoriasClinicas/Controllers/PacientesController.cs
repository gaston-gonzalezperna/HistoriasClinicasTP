using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace HistoriasClinicas2.Controllers
{
    public class PacientesController : Controller
    {
        private readonly EFContext _context;

        public PacientesController(EFContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Empleado, Medico")]
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.Pacientes.Include(p => p.Usuario);
            return View(await eFContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            
            if (validacionId(id))
            {
                return NotFound();
            }

            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }


        private bool validacionId(int? id) {

            if (User.IsInRole("Paciente"))
            {
                int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var paciente1 = _context.Pacientes.Where(m => m.Id == id).FirstOrDefault();
                
                if (paciente1 == null)
                {
                    return false;
                }

                if (userId != paciente1.UsuarioId)
                {
                    return true;
                }
                
            }
            return false;
        }


        // GET: Pacientes/Create
        //public IActionResult Create()
        //{
        //    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator");
        //    return View();
        //}

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ObraSocial,Id,Nombre,Apellido,DNI,Direccion,FechaAlta,UsuarioId")] Paciente paciente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(paciente);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        // //  ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", paciente.UsuarioId);
        //    return View(paciente);
        //}

        // GET: Pacientes/Edit/5

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            //ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", paciente.UsuarioId);
            return View(paciente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,DNI,Direccion,Telefono,Email,ObraSocial,UsuarioId")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    _context.SaveChanges();
                    return RedirectToAction("ActualizarEmail", "Accounts", new { nuevoEmail = paciente.Email, idUsuario = paciente.UsuarioId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = paciente.Id });
            }
            //      ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", paciente.UsuarioId);
            return RedirectToAction("Details", new { id = paciente.Id });
        }

        // GET: Pacientes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var paciente = await _context.Pacientes
        //        .Include(p => p.Usuario)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (paciente == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(paciente);
        //}

        //// POST: Pacientes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var paciente = await _context.Pacientes.FindAsync(id);
        //    _context.Pacientes.Remove(paciente);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("BorrarUsuario", "Accounts", new { email = paciente.Email });
        //}

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
