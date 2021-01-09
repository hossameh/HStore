using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class PurchaseRequest
    {
        public PurchaseRequest()
        {
            PurchaseRequestDetails = new HashSet<PurchaseRequestDetails>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remaining { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual ICollection<PurchaseRequestDetails> PurchaseRequestDetails { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
