using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            PurchaseRequest = new HashSet<PurchaseRequest>();
            SuppliersPayments = new HashSet<SuppliersPayments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public decimal? TotalPaid { get; set; }
        public decimal? TotalRemaining { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequest { get; set; }
        public virtual ICollection<SuppliersPayments> SuppliersPayments { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
