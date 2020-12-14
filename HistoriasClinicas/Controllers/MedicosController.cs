using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HistoriasClinicas2.Data;
using HistoriasClinicas2.Models;

namespace HistoriasClinicas.Controllers
{
    public class MedicosController : Controller
    {
        private readonly EFContext _context;

        public MedicosController(EFContext context)
        {
            _context = context;
        }

        // GET: Medicos
        public async Task<IActionResult> Index()
        {
            var eFContext = _context.Medicos.Include(m => m.Usuario);
            return View(await eFContext.ToListAsync());
        }

        // GET: Medicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medicos/Create
        //public IActionResult Create()
        //{
        //    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator");
        //    return View();
        //}

        // POST: Medicos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Matricula,Especialidad,Id,Nombre,Apellido,DNI,Direccion,FechaAlta,UsuarioId")] Medico medico)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(medico);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", medico.UsuarioId);
        //    return View(medico);
        //}

        // GET: Medicos/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medico = await _context.Medicos.FindAsync(id);
        //    if (medico == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", medico.UsuarioId);
        //    return View(medico);
        //}

        // POST: Medicos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Matricula,Especialidad,Id,Nombre,Apellido,DNI,Direccion,FechaAlta,UsuarioId")] Medico medico)
        //{
        //    if (id != medico.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(medico);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MedicoExists(medico.Id))
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
        //    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Discriminator", medico.UsuarioId);
        //    return View(medico);
        //}

        // GET: Medicos/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medico = await _context.Medicos
        //        .Include(m => m.Usuario)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (medico == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(medico);
        //}

        //// POST: Medicos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var medico = await _context.Medicos.FindAsync(id);
        //    _context.Medicos.Remove(medico);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MedicoExists(int id)
        //{
        //    return _context.Medicos.Any(e => e.Id == id);
        //}
    }
}
