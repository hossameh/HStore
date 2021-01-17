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
    public class ClientsPaymentsController : Controller
    {
        private readonly HStoreDBContext _context;

        public ClientsPaymentsController(HStoreDBContext context)
        {
            _context = context;
        }

        // GET: ClientsPayments
        public async Task<IActionResult> Index()
        {
            var hStoreDBContext = _context.ClientsPayments.Include(c => c.Client).Include(c => c.User);
            return View(await hStoreDBContext.ToListAsync());
        }

        // GET: ClientsPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsPayments = await _context.ClientsPayments
                .Include(c => c.Client)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsPayments == null)
            {
                return NotFound();
            }

            return View(clientsPayments);
        }

        // GET: ClientsPayments/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: ClientsPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentValue,PaymentDate,PaymentComment,ClientId,CreationDate,CreationBy,IsActive,UserId")] ClientsPayments clientsPayments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientsPayments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", clientsPayments.ClientId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", clientsPayments.UserId);
            return View(clientsPayments);
        }

        // GET: ClientsPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsPayments = await _context.ClientsPayments.FindAsync(id);
            if (clientsPayments == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", clientsPayments.ClientId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", clientsPayments.UserId);
            return View(clientsPayments);
        }

        // POST: ClientsPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentValue,PaymentDate,PaymentComment,ClientId,CreationDate,CreationBy,IsActive,UserId")] ClientsPayments clientsPayments)
        {
            if (id != clientsPayments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientsPayments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientsPaymentsExists(clientsPayments.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", clientsPayments.ClientId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", clientsPayments.UserId);
            return View(clientsPayments);
        }

        // GET: ClientsPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientsPayments = await _context.ClientsPayments
                .Include(c => c.Client)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientsPayments == null)
            {
                return NotFound();
            }

            return View(clientsPayments);
        }

        // POST: ClientsPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientsPayments = await _context.ClientsPayments.FindAsync(id);
            _context.ClientsPayments.Remove(clientsPayments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientsPaymentsExists(int id)
        {
            return _context.ClientsPayments.Any(e => e.Id == id);
        }
    }
}
