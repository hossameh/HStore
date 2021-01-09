using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class StoreItems
    {
        public StoreItems()
        {
            PurchaseRequestDetails = new HashSet<PurchaseRequestDetails>();
            SellRequestDetails = new HashSet<SellRequestDetails>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TodayPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? StorePrice { get; set; }
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<PurchaseRequestDetails> PurchaseRequestDetails { get; set; }
        public virtual ICollection<SellRequestDetails> SellRequestDetails { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
