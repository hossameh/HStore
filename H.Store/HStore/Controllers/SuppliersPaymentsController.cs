using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HStore;
using Microsoft.AspNetCore.Authorization;

namespace HStore.Controllers
{
    [Authorize]
    public class SuppliersPaymentsController : Controller
    {
        private readonly HStoreDBContext _context;

        public SuppliersPaymentsController(HStoreDBContext context)
        {
            _context = context;
        }

        // GET: SuppliersPayments
        public async Task<IActionResult> Index()
        {
            var hStoreDBContext = _context.SuppliersPayments.Include(s => s.Supplier).Include(s => s.User);
            return View(await hStoreDBContext.ToListAsync());
        }

        // GET: SuppliersPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersPayments = await _context.SuppliersPayments
                .Include(s => s.Supplier)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliersPayments == null)
            {
                return NotFound();
            }

            return View(suppliersPayments);
        }

        // GET: SuppliersPayments/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: SuppliersPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentValue,PaymentDate,PaymentComment,SupplierId,CreationDate,CreationBy,IsActive,UserId")] SuppliersPayments suppliersPayments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(suppliersPayments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", suppliersPayments.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", suppliersPayments.UserId);
            return View(suppliersPayments);
        }

        // GET: SuppliersPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersPayments = await _context.SuppliersPayments.FindAsync(id);
            if (suppliersPayments == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", suppliersPayments.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", suppliersPayments.UserId);
            return View(suppliersPayments);
        }

        // POST: SuppliersPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentValue,PaymentDate,PaymentComment,SupplierId,CreationDate,CreationBy,IsActive,UserId")] SuppliersPayments suppliersPayments)
        {
            if (id != suppliersPayments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suppliersPayments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuppliersPaymentsExists(suppliersPayments.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", suppliersPayments.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", suppliersPayments.UserId);
            return View(suppliersPayments);
        }

        // GET: SuppliersPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suppliersPayments = await _context.SuppliersPayments
                .Include(s => s.Supplier)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (suppliersPayments == null)
            {
                return NotFound();
            }

            return View(suppliersPayments);
        }

        // POST: SuppliersPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suppliersPayments = await _context.SuppliersPayments.FindAsync(id);
            _context.SuppliersPayments.Remove(suppliersPayments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuppliersPaymentsExists(int id)
        {
            return _context.SuppliersPayments.Any(e => e.Id == id);
        }
    }
}
