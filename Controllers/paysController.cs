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
    public class paysController : Controller
    {
        private readonly NestStudentContext _context;

        public paysController(NestStudentContext context)
        {
            _context = context;
        }

        // GET: pays
        public async Task<IActionResult> pays_student()
        {
            return _context.Pays != null ?
                        View(await _context.Pays.ToListAsync()) :
                        Problem("Entity set 'demoContext.pay'  is null.");
        }

        // GET: pays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            var pay = await _context.Pays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pay == null)
            {
                return NotFound();
            }

            return View(pay);
        }

        // GET: pays/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageData,ImageMimeType,ImageFile")] Pay pay)
        {
            if (ModelState.IsValid)
            {
                if (pay.ImageFile != null && pay.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pay.ImageFile.CopyToAsync(memoryStream);
                        pay.ImageData = memoryStream.ToArray();
                        pay.ImageMimeType = pay.ImageFile.ContentType;
                    }
                }

                _context.Add(pay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(pays_housemaster));
            }
            else
            {
                // 打印 ModelState 错误信息
                var errors = ModelState.SelectMany(x => x.Value.Errors)
                                       .Select(e => e.ErrorMessage)
                                       .ToList();
                ViewBag.Errors = errors;
            }
            return View(pay);
        }
        

        // GET: pays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            var pay = await _context.Pays.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }
            return View(pay);
        }

        // POST: pays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageData,ImageMimeType")] Pay pay)
        {
            if (id != pay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!payExists(pay.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(pays_student));
            }
            return View(pay);
        }

        // GET: pays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pays == null)
            {
                return NotFound();
            }

            var pay = await _context.Pays
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pay == null)
            {
                return NotFound();
            }

            return View(pay);
        }

        // POST: pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pays == null)
            {
                return Problem("Entity set 'demoContext.pay'  is null.");
            }
            var pay = await _context.Pays.FindAsync(id);
            if (pay != null)
            {
                _context.Pays.Remove(pay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(pays_housemaster));
        }
        public async Task<IActionResult> pays_housemaster()
        {
            return _context.Pays != null ?
                        View(await _context.Pays.ToListAsync()) :
                        Problem("Entity set 'demoContext.pay'  is null.");
        }
        public async Task<IActionResult> pays_housemaster_photo()
        {
            return _context.Pays != null ?
                        View(await _context.Pays.ToListAsync()) :
                        Problem("Entity set 'demoContext.pay'  is null.");
        }
       
        private bool payExists(int id)
        {
          return (_context.Pays?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
