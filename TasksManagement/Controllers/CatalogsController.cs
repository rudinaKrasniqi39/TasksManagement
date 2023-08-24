using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksManagement.Data;
using TasksManagement.Models;

namespace TasksManagement.Controllers
{
    public class CatalogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CatalogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.Catalogs != null ? 
                          View(await _context.Catalogs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Catalogs'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(catalog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(catalog);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog == null)
            {
                return NotFound();
            }
            return View(catalog);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Catalog catalog)
        {
            if (id != catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(catalog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CatalogExists(catalog.Id))
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
            return View(catalog);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Catalogs == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Catalogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Catalogs'  is null.");
            }
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog != null)
            {
                _context.Catalogs.Remove(catalog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(int id)
        {
          return (_context.Catalogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
