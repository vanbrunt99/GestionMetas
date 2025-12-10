using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionMetas.Data;
using GestionMetas.Models;

namespace GestionMetas.Controllers
{
    public class TareasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TareasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tareas
        public async Task<IActionResult> Index()
        {
            // Incluimos la Meta para poder mostrar el título en la tabla
            var tareas = await _context.Tareas
                .Include(t => t.Meta)
                .ToListAsync();

            return View(tareas);
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas
                .Include(t => t.Meta)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null) return NotFound();

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create(int? metaId)
        {
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Titulo", metaId);
            return View();
        }

        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hubo error de validación, volvemos a llenar el combo
            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Titulo", tarea.MetaId);
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null) return NotFound();

            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Titulo", tarea.MetaId);
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tarea tarea)
        {
            if (id != tarea.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tareas.Any(e => e.Id == tarea.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MetaId"] = new SelectList(_context.Metas, "Id", "Titulo", tarea.MetaId);
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tarea = await _context.Tareas
                .Include(t => t.Meta)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tarea == null) return NotFound();

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
