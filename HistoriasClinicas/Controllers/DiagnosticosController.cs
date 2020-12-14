using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HistoriasClinicas2.Controllers
{
    public class DiagnosticosController : Controller
    {
        private readonly EFContext _context;

        public DiagnosticosController(EFContext context)
        {
            _context = context;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.Diagnosticos.Include(d => d.Epicrisis);
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

            var diagnostico = await _context.Diagnosticos
                .Include(d => d.Epicrisis)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (diagnostico == null)
            {
                return NotFound();
            }

            return View(diagnostico);
        }

        [Authorize(Roles = "Empleado,Medico")]
        public IActionResult Create(int? idEpi)
        {
            ViewBag.EpicrisisId = idEpi;
            return View();
        }

        private bool validacionId(int? id)
        {

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descripcion,Recomendacion,EpicrisisId")] Diagnostico diagnostico)
        {
            if (ModelState.IsValid)
            {
                var epicrisis = await _context.Epicrisis
                .FirstOrDefaultAsync(m => m.Id == diagnostico.EpicrisisId);

                diagnostico.Epicrisis = epicrisis;

                if (User.IsInRole("Empleado"))
                {
                    diagnostico.Recomendacion = "CIERRE ADMINISTRATIVO";
                }

                var episodio = await _context.Episodios
                .FirstOrDefaultAsync(m => m.Id == epicrisis.EpisodioId);

                episodio.FechaYHoraCierre = DateTime.Now;


                _context.Update(episodio);
                _context.Add(diagnostico);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
           
            return View();
        }

        // GET: Diagnosticos/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var diagnostico = await _context.Diagnosticos.FindAsync(id);
        //    if (diagnostico == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
        //    return View(diagnostico);
        //}

        // POST: Diagnosticos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Recomendacion,EpicrisisId")] Diagnostico diagnostico)
        //{
        //    if (id != diagnostico.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(diagnostico);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DiagnosticoExists(diagnostico.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["EpicrisisId"] = new SelectList(_context.Epicrisis, "Id", "Id", diagnostico.EpicrisisId);
        //    return View(diagnostico);
        //}

        // GET: Diagnosticos/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var diagnostico = await _context.Diagnosticos
        //        .Include(d => d.Epicrisis)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (diagnostico == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(diagnostico);
        //}

        // POST: Diagnosticos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var diagnostico = await _context.Diagnosticos.FindAsync(id);
        //    _context.Diagnosticos.Remove(diagnostico);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool DiagnosticoExists(int id)
        {
            return _context.Diagnosticos.Any(e => e.Id == id);
        }
    }
}
