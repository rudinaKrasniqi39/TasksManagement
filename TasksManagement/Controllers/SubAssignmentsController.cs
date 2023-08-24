using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TasksManagement.Data;
using TasksManagement.Models;

namespace TasksManagement.Controllers
{
    public class SubAssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubAssignmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return _context.SubAssignments != null ? 
                          View(await _context.SubAssignments.Where(x=>!x.IsDeleted).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SubAssignments'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubAssignments == null)
            {
                return NotFound();
            }

            var subAssignment = await _context.SubAssignments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subAssignment == null)
            {
                return NotFound();
            }

            return View(subAssignment);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubAssignment subAssignment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subAssignment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subAssignment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubAssignments == null)
            {
                return NotFound();
            }

            var subAssignment = await _context.SubAssignments.FindAsync(id);
            if (subAssignment == null)
            {
                return NotFound();
            }
            return View(subAssignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubAssignment subAssignment)
        {
            if (id != subAssignment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subAssignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubAssignmentExists(subAssignment.Id))
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
            return View(subAssignment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubAssignments == null)
            {
                return NotFound();
            }

            var subAssignment = await _context.SubAssignments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subAssignment == null)
            {
                return NotFound();
            }

            return View(subAssignment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubAssignments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SubAssignments'  is null.");
            }
            var subAssignment = await _context.SubAssignments.FindAsync(id);
            if (subAssignment != null)
            {
                subAssignment.IsDeleted = true;
                _context.Update(subAssignment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubAssignmentExists(int id)
        {
          return (_context.SubAssignments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
