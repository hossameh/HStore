using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class PurchaseRequestDetails
    {
        public int Id { get; set; }
        public int PurchaseRequestId { get; set; }
        public int ItemId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? PurchaseQuantity { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual StoreItems Item { get; set; }
        public virtual PurchaseRequest PurchaseRequest { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
