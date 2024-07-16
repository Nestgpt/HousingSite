using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebA.Models;

namespace WebA.Controllers
{
    public class fixesController : Controller
    {
        private readonly NestStudentContext _context;

        public fixesController(NestStudentContext context)
        {
            _context = context;
        }

        // GET: fixes
        public async Task<IActionResult> fix_student()
        {
              return _context.Fixes != null ? 
                          View(await _context.Fixes.ToListAsync()) :
                          Problem("Entity set 'demoContext.fix'  is null.");
        }

        // GET: fixes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fixes == null)
            {
                return NotFound();
            }

            var fix = await _context.Fixes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fix == null)
            {
                return NotFound();
            }

            return View(fix);
        }

        // GET: fixes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: fixes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Project,Site")] Fix fix)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fix);
                await _context.SaveChangesAsync();
                return RedirectToAction("Student_page", "Main");
            }
            return View(fix);
        }

        // GET: fixes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fixes == null)
            {
                return NotFound();
            }

            var fix = await _context.Fixes.FindAsync(id);
            if (fix == null)
            {
                return NotFound();
            }
            return View(fix);
        }

        // POST: fixes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Project,Site,SelectedOption")] Fix fix)
        {
            if (id != fix.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fix);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!fixExists(fix.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(fix_housemaster));
            }
            return View(fix);
        }

        // GET: fixes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fixes == null)
            {
                return NotFound();
            }

            var fix = await _context.Fixes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fix == null)
            {
                return NotFound();
            }

            return View(fix);
        }

        // POST: fixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fixes == null)
            {
                return Problem("Entity set 'demoContext.fix'  is null.");
            }
            var fix = await _context.Fixes.FindAsync(id);
            if (fix != null)
            {
                _context.Fixes.Remove(fix);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(fix_housemaster));
        }
        public async Task<IActionResult> fix_housemaster()
        {
            return _context.Fixes != null ?
                        View(await _context.Fixes.ToListAsync()) :
                        Problem("Entity set 'demoContext.fix'  is null.");
        }
        
        [HttpPost]
     

        private bool fixExists(int id)
        {
          return (_context.Fixes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
