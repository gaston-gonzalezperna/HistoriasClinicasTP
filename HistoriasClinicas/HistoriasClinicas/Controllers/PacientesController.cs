using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas.Data;
using HistoriasClinicas.Models;
using HistoriasClinicas.ViewModels;

namespace HistoriasClinicas.Controllers
{
    public class PacientesController : Controller
    {
        private readonly EFContext _context;

        public PacientesController(EFContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            PacienteDto model = new PacienteDto();
            model.Id = paciente.Id;
            model.Nombre = paciente.Nombre;
            model.Apellido = paciente.Apellido;
            model.DNI = paciente.DNI;
            model.Direccion = paciente.Direccion;
            model.Telefono = paciente.PhoneNumber;
            model.ObraSocial = paciente.ObraSocial;
            model.Email = paciente.Email;

            return View(model);
        }

        // GET: Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObraSocial,Nombre,Apellido,DNI,Direccion,FechaAlta,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
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

            PacienteDto model = new PacienteDto();
            model.Id = paciente.Id;
            model.Nombre = paciente.Nombre;
            model.Apellido = paciente.Apellido;
            model.DNI = paciente.DNI;
            model.Direccion = paciente.Direccion;
            model.Telefono = paciente.PhoneNumber;
            model.ObraSocial = paciente.ObraSocial;
            model.Email = paciente.Email;

            return View(model);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Apellido,DNI,Direccion,Telefono,ObraSocial,Email")] PacienteDto model)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }            

            if (ModelState.IsValid)
            {
                paciente.Nombre = model.Nombre;
                paciente.Apellido = model.Apellido;
                paciente.DNI = model.DNI;
                paciente.Direccion = model.Direccion;
                paciente.PhoneNumber = model.Telefono;
                paciente.ObraSocial = model.ObraSocial;
                paciente.Email = model.Email;
                paciente.NormalizedEmail = model.Email.ToUpper();
                paciente.UserName = model.Email;
                paciente.NormalizedUserName = model.Email.ToUpper();

                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction("Details", new { id = id });
            }

            return RedirectToAction("Details", new { id = id });
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}
