using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HStore.ViewModels
{
    public class SellRequestVM
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? SellDate { get; set; }
        public int ClientId { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remaining { get; set; }
        public  List<SellRequesttDetailsVM> SellRequestDetails { get; set; }
    }
    public class SellRequesttDetailsVM
    {
        public int ItemId { get; set; }
        public decimal? SellPrice { get; set; }
        public int? SellQuantity { get; set; }
    }
}
