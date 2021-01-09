using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class SellRequestDetails
    {
        public int Id { get; set; }
        public int SellRequestId { get; set; }
        public int ItemId { get; set; }
        public decimal? SellPrice { get; set; }
        public int? SellQuantity { get; set; }
        public decimal? SellTotalAmount { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual StoreItems Item { get; set; }
        public virtual SellRequest SellRequest { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
