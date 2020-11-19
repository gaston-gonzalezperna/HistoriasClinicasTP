using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas.Data;
using HistoriasClinicas.Models;

namespace HistoriasClinicas.Controllers
{
    public class HistoriasClinicasController : Controller
    {
        private readonly EFContext _context;

        public HistoriasClinicasController(EFContext context)
        {
            _context = context;
        }

        // GET: HistoriasClinicas
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.HistoriaClinicas.Include(h => h.Paciente);
            return View(await eFContext.ToListAsync());
        }

        // GET: HistoriasClinicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = _context.HistoriaClinicas.First(h => h.IdPaciente == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Create
        public IActionResult Create()
        {
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Discriminator");
            return View();
        }

        // POST: HistoriasClinicas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPaciente")] HistoriaClinica historiaClinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historiaClinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Discriminator", historiaClinica.IdPaciente);
            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            if (historiaClinica == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Discriminator", historiaClinica.IdPaciente);
            return View(historiaClinica);
        }

        // POST: HistoriasClinicas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPaciente")] HistoriaClinica historiaClinica)
        {
            if (id != historiaClinica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historiaClinica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoriaClinicaExists(historiaClinica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Pacientes, "Id", "Discriminator", historiaClinica.IdPaciente);
            return View(historiaClinica);
        }

        // GET: HistoriasClinicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historiaClinica = await _context.HistoriaClinicas
                .Include(h => h.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (historiaClinica == null)
            {
                return NotFound();
            }

            return View(historiaClinica);
        }

        // POST: HistoriasClinicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historiaClinica = await _context.HistoriaClinicas.FindAsync(id);
            _context.HistoriaClinicas.Remove(historiaClinica);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoriaClinicaExists(int id)
        {
            return _context.HistoriaClinicas.Any(e => e.Id == id);
        }
    }
}
