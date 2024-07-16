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
    public class AskForLeaveController : Controller
    {
        private readonly NestStudentContext _context;

        public AskForLeaveController(NestStudentContext context)
        {
            _context = context;
        }

        // GET: AskForLeave
       
        public async Task<IActionResult> askforleave_housemaster()
        {
            return View(await _context.AskForLeaves.ToListAsync());
        }
        public async Task<IActionResult> askforleave_student()
        {
            return View(await _context.AskForLeaves.ToListAsync());
        }

        // GET: AskForLeave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var askForLeave = await _context.AskForLeaves
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (askForLeave == null)
            {
                return NotFound();
            }

            return View(askForLeave);
        }

        // GET: AskForLeave/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Reason,Date,SelectedOption")] AskForLeave askForLeave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(askForLeave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(askforleave_student));
            }
            return View(askForLeave);
        }

        // GET: AskForLeave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var askForLeave = await _context.AskForLeaves.FirstOrDefaultAsync(m => m.AskForLeaveId == id);
            if (askForLeave == null)
            {
                return NotFound();
            }
            return View(askForLeave);
        }

        // POST: AskForLeave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AskForLeaveId,StudentId,Reason,Date,SelectedOption")] AskForLeave askForLeave)
        {
            if (id != askForLeave.AskForLeaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(askForLeave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AskForLeaveExists(askForLeave.AskForLeaveId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(askforleave_housemaster));
            }
            return View(askForLeave);
        }

        // GET: AskForLeave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var askForLeave = await _context.AskForLeaves
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (askForLeave == null)
            {
                return NotFound();
            }

            return View(askForLeave);
        }

        // POST: AskForLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var askForLeave = await _context.AskForLeaves.FindAsync(id);
            if (askForLeave != null)
            {
                _context.AskForLeaves.Remove(askForLeave);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(askforleave_student));
        }

       
       
        private bool AskForLeaveExists(int id)
        {
            return _context.AskForLeaves.Any(e => e.StudentId == id);
        }
    }
}
