using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HStore;
using Microsoft.AspNetCore.Identity;
using HStore.ViewModels;

namespace HStore.Controllers
{
    public class PurchaseRequestsController : Controller
    {
        private readonly HStoreDBContext _context;
        private UserManager<IdentityUser> _userManager;

        public PurchaseRequestsController(HStoreDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                .Include(P => P.PurchaseRequestDetails)
                .ThenInclude(p => p.Item)
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
            ViewData["Items"] = new SelectList(_context.StoreItems, "Id", "Name");
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

        [HttpPost]
        public async Task<IActionResult> CreatePR(PurchaseRequestVM requestVM)//, string purchasedate, string supplierid, string paid, string remaining)//, [Bind("ItemId", "PurchasePrice", "PurchaseQuantity")] PurchaseRequestDetails[] purchaserequestdetails)
        {
            //if (requestVM != null)
            //{ PurchaseRequestVM itm = (PurchaseRequestVM)requestVM; } 
            string result = "Error! Order Is Not Complete! " + requestVM.InvoiceNumber;
            if (requestVM != null && requestVM.PurchaseRequestDetails != null)
            {
                PurchaseRequest purchaseRequest = new PurchaseRequest();
                purchaseRequest.InvoiceNumber = requestVM.InvoiceNumber;
                purchaseRequest.SupplierId = requestVM.SupplierId;
                purchaseRequest.Paid = requestVM.Paid;
                purchaseRequest.Remaining = requestVM.Remaining;
                purchaseRequest.CreationDate = DateTime.Now;
                purchaseRequest.PurchaseDate = requestVM.PurchaseDate;
                purchaseRequest.UserId = _userManager.GetUserId(User);
                var Pr = _context.PurchaseRequest.Add(purchaseRequest);

                // add header success
                if (await _context.SaveChangesAsync() > 0)
                {
                    // 1- Update Customer Balances
                    var supp = _context.Suppliers.FirstOrDefault(sId => sId.Id == requestVM.SupplierId);
                    if (supp!=null)
                    {
                        supp.TotalPaid = (supp.TotalPaid == null ? 0 : supp.TotalPaid) + requestVM.Paid;
                        supp.TotalRemaining = (supp.TotalRemaining == null ? 0 : supp.TotalRemaining) + requestVM.Remaining;
                        _context.Suppliers.Update(supp);
                    }
                    

                    foreach (var item in requestVM.PurchaseRequestDetails)
                    {
                        PurchaseRequestDetails requestDetails = new PurchaseRequestDetails();
                        requestDetails.ItemId = item.ItemId;
                        requestDetails.PurchasePrice = item.PurchasePrice;
                        requestDetails.PurchaseQuantity = item.PurchaseQuantity;
                        requestDetails.CreationDate = DateTime.Now;
                        requestDetails.PurchaseRequestId = Pr.Entity.Id;
                        requestDetails.UserId = _userManager.GetUserId(User);

                        _context.PurchaseRequestDetails.Add(requestDetails);

                        // 2- Update Items Balances
                        var Newitem = _context.StoreItems.FirstOrDefault(sId => sId.Id == item.ItemId);
                        if (Newitem != null)
                        {
                            Newitem.TodayPrice = item.PurchasePrice;
                            Newitem.Quantity = (Newitem.Quantity == null ? 0 : Newitem.Quantity) + item.PurchaseQuantity;
                            _context.StoreItems.Update(Newitem);
                        }

                    }
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        result = "Success! Order Is Complete!";
                    }
                }
            }
            
            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> EditPR(PurchaseRequestVM requestVM)//, string purchasedate, string supplierid, string paid, string remaining)//, [Bind("ItemId", "PurchasePrice", "PurchaseQuantity")] PurchaseRequestDetails[] purchaserequestdetails)
        {
            //if (requestVM != null)
            //{ PurchaseRequestVM itm = (PurchaseRequestVM)requestVM; } 
            string result = "Error! Order Is Not Complete! " + requestVM.InvoiceNumber;
            if (requestVM != null && requestVM.PurchaseRequestDetails != null)
            {
                PurchaseRequest purchaseRequest = await _context.PurchaseRequest.Include(p => p.PurchaseRequestDetails).ThenInclude(p => p.Item).FirstOrDefaultAsync(r => r.Id == requestVM.Id);
                var oldPaid = purchaseRequest.Paid;
                var oldRemaining = purchaseRequest.Remaining;

                if (purchaseRequest == null)
                {
                   return Json(result);
                }
                purchaseRequest.InvoiceNumber = requestVM.InvoiceNumber;
                purchaseRequest.Paid = requestVM.Paid;
                purchaseRequest.Remaining = requestVM.Remaining;
                purchaseRequest.PurchaseDate = requestVM.PurchaseDate;
                purchaseRequest.UserId = _userManager.GetUserId(User);
                var Pr = _context.PurchaseRequest.Update(purchaseRequest);

                // add header success
                if (await _context.SaveChangesAsync() > 0)
                {
                    // 1- Update Customer Balances
                    var supp = _context.Suppliers.FirstOrDefault(sId => sId.Id == requestVM.SupplierId);
                    if (supp != null)
                    {
                        supp.TotalPaid = (supp.TotalPaid == null ? 0 : supp.TotalPaid) + requestVM.Paid - (oldPaid == null ? 0 : oldPaid);
                        supp.TotalRemaining = (supp.TotalRemaining == null ? 0 : supp.TotalRemaining) + requestVM.Remaining - (oldRemaining == null ? 0 : oldRemaining); 
                        _context.Suppliers.Update(supp);
                    }


                    foreach (var item in requestVM.PurchaseRequestDetails)
                    {
                        PurchaseRequestDetails requestDetails = new PurchaseRequestDetails();
                        requestDetails.ItemId = item.ItemId;
                        requestDetails.PurchasePrice = item.PurchasePrice;
                        requestDetails.PurchaseQuantity = item.PurchaseQuantity;
                        requestDetails.PurchaseRequestId = Pr.Entity.Id;
                        requestDetails.UserId = _userManager.GetUserId(User);

                        _context.PurchaseRequestDetails.Add(requestDetails);

                        // 2- Update Items Balances
                        var Newitem = _context.StoreItems.FirstOrDefault(sId => sId.Id == item.ItemId);
                        if (Newitem != null)
                        {
                            Newitem.TodayPrice = item.PurchasePrice;
                            Newitem.Quantity = (Newitem.Quantity == null ? 0 : Newitem.Quantity) + item.PurchaseQuantity;
                            _context.StoreItems.Update(Newitem);
                        }

                    }
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        result = "Success! Order Is Complete!";
                    }
                }
            }

            return Json(result);

        }
        // GET: PurchaseRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRequest = await _context.PurchaseRequest.Include(P => P.PurchaseRequestDetails).ThenInclude(p=>p.Item).FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", purchaseRequest.SupplierId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", purchaseRequest.UserId);
            ViewData["Items"] = new SelectList(_context.StoreItems, "Id", "Name");
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
                .Include(P => P.PurchaseRequestDetails)
                .ThenInclude(p => p.Item)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseRequest == null)
            {
                return NotFound();
            }

            return View(purchaseRequest);
        }

        public async Task<IActionResult> DeletePRDetails(int? id)
        {
            string result = "Error! Delete Item Is Not Complete! ";
            if (id == null)
            {
                return NotFound();
            }
            var PrDetails = await _context.PurchaseRequestDetails.FindAsync(id);
            if (PrDetails == null)
            {
                return NotFound();
            }
           
                var itm =await _context.StoreItems.FirstOrDefaultAsync(itmId => itmId.Id == PrDetails.ItemId);
                if (itm != null)
                {
                    itm.Quantity = (itm.Quantity == null ? 0 : itm.Quantity) - PrDetails.PurchaseQuantity;
                    _context.StoreItems.Update(itm);
                }

            _context.PurchaseRequestDetails.Remove(PrDetails);
            if (await _context.SaveChangesAsync() > 0)
                result = "Ok";
            return Json(result);
        }
        // POST: PurchaseRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseRequest = await _context.PurchaseRequest.FindAsync(id);
            
            var supp = _context.Suppliers.FirstOrDefault(supId => supId.Id == purchaseRequest.SupplierId);
            List<PurchaseRequestDetails> PrDetails = _context.PurchaseRequestDetails.Where(prId => prId.PurchaseRequestId == id).ToList();
            foreach (var prdItem in PrDetails)
            {
                var itm = _context.StoreItems.FirstOrDefault(itmId => itmId.Id == prdItem.ItemId);
                if (itm!=null)
                {
                    itm.Quantity = (itm.Quantity == null ? 0 : itm.Quantity) - prdItem.PurchaseQuantity;
                    _context.StoreItems.Update(itm);
                }
            }
            _context.PurchaseRequestDetails.RemoveRange(PrDetails);
            supp.TotalPaid = (supp.TotalPaid == null ? 0 : supp.TotalPaid) - purchaseRequest.Paid;
            supp.TotalRemaining = (supp.TotalRemaining == null ? 0 : supp.TotalRemaining) - purchaseRequest.Remaining;
            _context.Suppliers.Update(supp);

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
