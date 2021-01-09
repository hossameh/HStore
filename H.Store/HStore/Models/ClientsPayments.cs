using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class ClientsPayments
    {
        public int Id { get; set; }
        public decimal PaymentValue { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentComment { get; set; }
        public int ClientId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? CreationBy { get; set; }
        public bool? IsActive { get; set; }
        public string UserId { get; set; }
        public virtual Clients Client { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
