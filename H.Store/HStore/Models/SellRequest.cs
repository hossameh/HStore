using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class SellRequest
    {
        public SellRequest()
        {
            SellRequestDetails = new HashSet<SellRequestDetails>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? SellDate { get; set; }
        public int ClientId { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remaining { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual Clients Client { get; set; }
        public virtual ICollection<SellRequestDetails> SellRequestDetails { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
