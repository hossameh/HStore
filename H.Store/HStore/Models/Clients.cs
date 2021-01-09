using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class Clients
    {
        public Clients()
        {
            ClientsPayments = new HashSet<ClientsPayments>();
            SellRequest = new HashSet<SellRequest>();
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
        public virtual ICollection<ClientsPayments> ClientsPayments { get; set; }
        public virtual ICollection<SellRequest> SellRequest { get; set; }
        public virtual AspNetUsers User { get; set; }

    }
}
