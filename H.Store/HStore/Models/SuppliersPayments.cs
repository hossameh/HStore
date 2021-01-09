using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class SuppliersPayments
    {
        public int Id { get; set; }
        public decimal PaymentValue { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentComment { get; set; }
        public int SupplierId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual Suppliers Supplier { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
