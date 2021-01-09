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
    public class StoreItemsController : Controller
    {
        private readonly HStoreDBContext _context;

        public StoreItemsController(HStoreDBContext context)
        {
            _context = context;
        }

        // GET: StoreItems
        public async Task<IActionResult> Index()
        {
            var hStoreDBContext = _context.StoreItems.Include(s => s.User);
            return View(await hStoreDBContext.ToListAsync());
        }

        // GET: StoreItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.StoreItems
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeItems == null)
            {
                return NotFound();
            }

            return View(storeItems);
        }

        // GET: StoreItems/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: StoreItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,Description,TodayPrice,Quantity,StorePrice,ProductionDate,ExpirationDate,CreationDate,CreationBy,IsActive,UserId")] StoreItems storeItems)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storeItems);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", storeItems.UserId);
            return View(storeItems);
        }

        // GET: StoreItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.StoreItems.FindAsync(id);
            if (storeItems == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", storeItems.UserId);
            return View(storeItems);
        }

        // POST: StoreItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,Description,TodayPrice,Quantity,StorePrice,ProductionDate,ExpirationDate,CreationDate,CreationBy,IsActive,UserId")] StoreItems storeItems)
        {
            if (id != storeItems.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storeItems);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoreItemsExists(storeItems.Id))
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
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", storeItems.UserId);
            return View(storeItems);
        }

        // GET: StoreItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storeItems = await _context.StoreItems
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storeItems == null)
            {
                return NotFound();
            }

            return View(storeItems);
        }

        // POST: StoreItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storeItems = await _context.StoreItems.FindAsync(id);
            _context.StoreItems.Remove(storeItems);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreItemsExists(int id)
        {
            return _context.StoreItems.Any(e => e.Id == id);
        }
    }
}
