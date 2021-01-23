using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HStore;
using HStore.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace HStore.Controllers
{
    public class SellRequestsController : Controller
    {
        private readonly HStoreDBContext _context;
        private UserManager<IdentityUser> _userManager;
        public SellRequestsController(HStoreDBContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: SellRequests
        public async Task<IActionResult> Index()
        {
            var hStoreDBContext = _context.SellRequest.Include(s => s.Client).Include(s => s.User);
            return View(await hStoreDBContext.ToListAsync());
        }

        // GET: SellRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellRequest = await _context.SellRequest
                .Include(s => s.Client)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellRequest == null)
            {
                return NotFound();
            }

            return View(sellRequest);
        }

        // GET: SellRequests/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["Items"] = new SelectList(_context.StoreItems, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSR(SellRequestVM requestVM)//, string purchasedate, string supplierid, string paid, string remaining)//, [Bind("ItemId", "PurchasePrice", "PurchaseQuantity")] PurchaseRequestDetails[] purchaserequestdetails)
        {
            //if (requestVM != null)
            //{ PurchaseRequestVM itm = (PurchaseRequestVM)requestVM; } 
            string result = "Error! Order Is Not Complete! " + requestVM.InvoiceNumber;
            if (requestVM != null && requestVM.SellRequestDetails != null)
            {
                // check item balance 
                var msg = string.Empty;
                if (!checkItemBalances(requestVM.SellRequestDetails, out msg))
                    return Json(msg);

                SellRequest sellRequest = new SellRequest();
                sellRequest.InvoiceNumber = requestVM.InvoiceNumber;
                sellRequest.ClientId = requestVM.ClientId;
                sellRequest.Paid = requestVM.Paid;
                sellRequest.Remaining = requestVM.Remaining;
                sellRequest.CreationDate = DateTime.Now;
                sellRequest.SellDate = requestVM.SellDate;
                sellRequest.UserId = _userManager.GetUserId(User);
                var Pr = _context.SellRequest.Add(sellRequest);

                // add header success
                if (await _context.SaveChangesAsync() > 0)
                {
                    // 1- Update Customer Balances
                    var client = _context.Clients.FirstOrDefault(sId => sId.Id == requestVM.ClientId);
                    if (client != null)
                    {
                        client.TotalPaid = (client.TotalPaid == null ? 0 : client.TotalPaid) + requestVM.Paid;
                        client.TotalRemaining = (client.TotalRemaining == null ? 0 : client.TotalRemaining) + requestVM.Remaining;
                        _context.Clients.Update(client);
                    }

                   

                    foreach (var item in requestVM.SellRequestDetails)
                    {
                        SellRequestDetails requestDetails = new SellRequestDetails();
                        requestDetails.ItemId = item.ItemId;
                        requestDetails.SellPrice = item.SellPrice;
                        requestDetails.SellQuantity = item.SellQuantity;
                        requestDetails.CreationDate = DateTime.Now;
                        requestDetails.SellRequestId = Pr.Entity.Id;
                        requestDetails.UserId = _userManager.GetUserId(User);

                        _context.SellRequestDetails.Add(requestDetails);

                        // 2- Update Items Balances
                        var Newitem = _context.StoreItems.FirstOrDefault(sId => sId.Id == item.ItemId);
                        if (Newitem != null)
                        {
                            Newitem.Quantity = (Newitem.Quantity == null ? 0 : Newitem.Quantity) - item.SellQuantity;
                            _context.StoreItems.Update(Newitem);
                        }

                    }
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        result = "Ok";
                    }
                }
            }

            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> EditSR(SellRequestVM requestVM)//, string purchasedate, string supplierid, string paid, string remaining)//, [Bind("ItemId", "PurchasePrice", "PurchaseQuantity")] PurchaseRequestDetails[] purchaserequestdetails)
        {
            //if (requestVM != null)
            //{ PurchaseRequestVM itm = (PurchaseRequestVM)requestVM; } 
            string result = "Error! Order Is Not Complete! " + requestVM.InvoiceNumber;
            if (requestVM != null && requestVM.SellRequestDetails != null)
            {
                // check item balance 
                var msg = string.Empty;
                if (!checkItemBalances(requestVM.SellRequestDetails, out msg))
                    return Json(msg);

                SellRequest sellRequest = await _context.SellRequest.Include(p => p.SellRequestDetails).ThenInclude(p => p.Item).FirstOrDefaultAsync(r => r.Id == requestVM.Id);
                var oldPaid = sellRequest.Paid;
                var oldRemaining = sellRequest.Remaining;

                if (sellRequest == null)
                {
                    return Json(result);
                }
                sellRequest.InvoiceNumber = requestVM.InvoiceNumber;
                sellRequest.ClientId = requestVM.ClientId;
                sellRequest.Paid = requestVM.Paid;
                sellRequest.Remaining = requestVM.Remaining;
                sellRequest.SellDate = requestVM.SellDate;
                sellRequest.UserId = _userManager.GetUserId(User);
                var Pr = _context.SellRequest.Update(sellRequest);

                // add header success
                if (await _context.SaveChangesAsync() > 0)
                {
                    // 1- Update Customer Balances
                    var client = _context.Clients.FirstOrDefault(sId => sId.Id == requestVM.ClientId);
                    if (client != null)
                    {
                        client.TotalPaid = (client.TotalPaid == null ? 0 : client.TotalPaid) + requestVM.Paid - (oldPaid == null ? 0 : oldPaid);
                        client.TotalRemaining = (client.TotalRemaining == null ? 0 : client.TotalRemaining) + requestVM.Remaining - (oldRemaining == null ? 0 : oldRemaining);
                        _context.Clients.Update(client);
                    }



                    foreach (var item in requestVM.SellRequestDetails)
                    {
                        SellRequestDetails requestDetails = new SellRequestDetails();
                        requestDetails.ItemId = item.ItemId;
                        requestDetails.SellPrice = item.SellPrice;
                        requestDetails.SellQuantity = item.SellQuantity;
                        requestDetails.CreationDate = DateTime.Now;
                        requestDetails.SellRequestId = Pr.Entity.Id;
                        requestDetails.UserId = _userManager.GetUserId(User);

                        _context.SellRequestDetails.Add(requestDetails);

                        // 2- Update Items Balances
                        var Newitem = _context.StoreItems.FirstOrDefault(sId => sId.Id == item.ItemId);
                        if (Newitem != null)
                        {
                            Newitem.Quantity = (Newitem.Quantity == null ? 0 : Newitem.Quantity) - item.SellQuantity;
                            _context.StoreItems.Update(Newitem);
                        }

                    }
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        result = "Ok";
                    }
                }
            }

            return Json(result);

        }

        public async Task<IActionResult> DeleteSRDetails(int? id)
        {
            string result = "Error! Delete Item Is Not Complete! ";
            if (id == null)
            {
                return NotFound();
            }
            var SrDetails = await _context.SellRequestDetails.FindAsync(id);
            if (SrDetails == null)
            {
                return NotFound();
            }

            var itm = await _context.StoreItems.FirstOrDefaultAsync(itmId => itmId.Id == SrDetails.ItemId);
            if (itm != null)
            {
                itm.Quantity = (itm.Quantity == null ? 0 : itm.Quantity) + SrDetails.SellQuantity;
                _context.StoreItems.Update(itm);
            }

            _context.SellRequestDetails.Remove(SrDetails);
            if (await _context.SaveChangesAsync() > 0)
                result = "Ok";
            return Json(result);
        }
        private bool checkItemBalances(List<SellRequesttDetailsVM> sellRequestDetails, out string msg)
        {
            bool retVal = true;
            msg = "Ok";
            decimal availableBalance = 0;
            // Key = ItemId , Value= needed quantity
            var queryGroupBySum = sellRequestDetails.GroupBy(id => id.ItemId).Select(g => new
            {
                itemId = g.Key,
                itemQuantity = g.Sum(s => s.SellQuantity),
            });
            foreach (var item in queryGroupBySum)
            {
                var storeItem = _context.StoreItems.Find(item.itemId);
                if (storeItem == null)
                    return false;
                decimal.TryParse((storeItem.Quantity==null?"0": storeItem.Quantity.ToString()),out availableBalance);
                if (availableBalance < item.itemQuantity)
                {
                    msg = "Sorry, The available Quantity of Item "+storeItem.Name+" is "+availableBalance+" while the Sell request contains "+item.itemQuantity;
                    return false;
                }
            }
            return retVal;

        }

        // POST: SellRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceNumber,SellDate,ClientId,Paid,Remaining,TotalAmount,CreationDate,CreationBy,IsActive,UserId")] SellRequest sellRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sellRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", sellRequest.UserId);
            return View(sellRequest);
        }

        // GET: SellRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellRequest = await _context.SellRequest.Include(P => P.SellRequestDetails).ThenInclude(p => p.Item).FirstOrDefaultAsync(m => m.Id == id);
            if (sellRequest == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", sellRequest.ClientId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", sellRequest.UserId);
            ViewData["Items"] = new SelectList(_context.StoreItems, "Id", "Name");
            return View(sellRequest);
        }

        // POST: SellRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InvoiceNumber,SellDate,ClientId,Paid,Remaining,TotalAmount,CreationDate,CreationBy,IsActive,UserId")] SellRequest sellRequest)
        {
            if (id != sellRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sellRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SellRequestExists(sellRequest.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Name", sellRequest.ClientId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", sellRequest.UserId);
            return View(sellRequest);
        }

        // GET: SellRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sellRequest = await _context.SellRequest
                .Include(s => s.Client)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sellRequest == null)
            {
                return NotFound();
            }

            return View(sellRequest);
        }

        // POST: SellRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sellRequest = await _context.SellRequest.FindAsync(id);

            var client = _context.Clients.FirstOrDefault(supId => supId.Id == sellRequest.ClientId);
            List<SellRequestDetails> SrDetails = _context.SellRequestDetails.Where(prId => prId.SellRequestId == id).ToList();
            foreach (var srdItem in SrDetails)
            {
                var itm = _context.StoreItems.FirstOrDefault(itmId => itmId.Id == srdItem.ItemId);
                if (itm != null)
                {
                    itm.Quantity = (itm.Quantity == null ? 0 : itm.Quantity) + srdItem.SellQuantity;
                    _context.StoreItems.Update(itm);
                }
            }
            _context.SellRequestDetails.RemoveRange(SrDetails);
            client.TotalPaid = (client.TotalPaid == null ? 0 : client.TotalPaid) - sellRequest.Paid;
            client.TotalRemaining = (client.TotalRemaining == null ? 0 : client.TotalRemaining) - sellRequest.Remaining;
            _context.Clients.Update(client);

            _context.SellRequest.Remove(sellRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SellRequestExists(int id)
        {
            return _context.SellRequest.Any(e => e.Id == id);
        }
    }
}
