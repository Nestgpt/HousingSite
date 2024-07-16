using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebA.Models;

namespace demo.Controllers
{
    public class suesController : Controller
    {
        private readonly NestStudentContext _context;

        public suesController(NestStudentContext context)
        {
            _context = context;
        }

        // GET: sues
       
        public async Task<IActionResult> sues_housemaster()
        {
            return _context.Sues != null ?
                        View(await _context.Sues.ToListAsync()) :
                        Problem("Entity set 'demoContext.sue'  is null.");
        }
        public async Task<IActionResult> sues_student()
        {
            return _context.Sues != null ?
                        View(await _context.Sues.ToListAsync()) :
                        Problem("Entity set 'demoContext.sue'  is null.");
        }
        // GET: sues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sues == null)
            {
                return NotFound();
            }

            var sue = await _context.Sues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sue == null)
            {
                return NotFound();
            }

            return View(sue);
        }

        // GET: sues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: sues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Reason,Roomnumber")] Sue sue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sue);
                await _context.SaveChangesAsync();
                return RedirectToAction("Student_page", "Main");
            }
            return View(sue);
        }

        // GET: sues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sues == null)
            {
                return NotFound();
            }

            var sue = await _context.Sues.FindAsync(id);
            if (sue == null)
            {
                return NotFound();
            }
            return View(sue);
        }

        // POST: sues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Reason,Roomnumber,SelectedOption")] Sue sue)
        {
            if (id != sue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!sueExists(sue.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(sues_housemaster));
            }
            return View(sue);
        }

        // GET: sues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sues == null)
            {
                return NotFound();
            }

            var sue = await _context.Sues
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sue == null)
            {
                return NotFound();
            }

            return View(sue);
        }

        // POST: sues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sues == null)
            {
                return Problem("Entity set 'demoContext.sue'  is null.");
            }
            var sue = await _context.Sues.FindAsync(id);
            if (sue != null)
            {
                _context.Sues.Remove(sue);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(sues_student));
        }
       

        private bool sueExists(int id)
        {
          return (_context.Sues?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
