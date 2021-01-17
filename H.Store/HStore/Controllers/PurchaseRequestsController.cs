using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HStore;

namespace HStore.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private readonly HStoreDBContext _context;

        public PurchaseRequestsController(HStoreDBContext context)
        {
            _context = context;
        }

        // GET: PurchaseRequests
        public async Task<IActionResult> Index()
        {
            var hStoreDBContext = _context.PurchaseRequest.Include(p => p.Supplier).Include(p => p.User);
            return View(await hStoreDBContext.ToListAsync());
        }

        // GET: PurchaseRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequest
                .Include(p => p.Supplier)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: PurchaseRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceNumber,PurchaseDate,SupplierId,Paid,Remaining,TotalAmount,CreationDate,CreationBy,IsActive,UserId")] PurchaseRequest purchaseRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", purchaseRequest.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequest.FindAsync(id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", purchaseRequest.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNumber,PurchaseDate,SupplierId,Paid,Remaining,TotalAmount,CreationDate,CreationBy,IsActive,UserId")] PurchaseRequest purchaseRequest)
        {
            if (id != purchaseRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRequestExists(purchaseRequest.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Id", purchaseRequest.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", purchaseRequest.UserId);
            return View(purchaseRequest);
        }

        // GET: PurchaseRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequest
                .Include(p => p.Supplier)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return View(purchaseRequest);
        }

        // POST: PurchaseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseRequest = await _context.PurchaseRequest.FindAsync(id);
            _context.PurchaseRequest.Remove(purchaseRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRequestExists(int id)
        {
            return _context.PurchaseRequest.Any(e => e.Id == id);
        }
    }
}
