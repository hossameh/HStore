﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HStore.ViewModels
{
    public class PurchaseRequestVM
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public int SupplierId { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remaining { get; set; }
        public  List<PurchaseRequestDetailsVM> PurchaseRequestDetails { get; set; }
    }
    public class PurchaseRequestDetailsVM {
        public int ItemId { get; set; }
        public decimal? PurchasePrice { get; set; }
        public int? PurchaseQuantity { get; set; }
    }
}
