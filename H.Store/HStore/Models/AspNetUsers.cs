using System;
using System.Collections.Generic;

namespace HStore
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Clients = new HashSet<Clients>();
            ClientsPayments = new HashSet<ClientsPayments>();
            PurchaseRequests = new HashSet<PurchaseRequest>();
            PurchaseRequestDetails = new HashSet<PurchaseRequestDetails>();
            SellRequests = new HashSet<SellRequest>();
            SellRequestDetails = new HashSet<SellRequestDetails>();
            StoreItems = new HashSet<StoreItems>();
            Suppliers = new HashSet<Suppliers>();
            SuppliersPayments = new HashSet<SuppliersPayments>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<ClientsPayments> ClientsPayments { get; set; }
        public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; }
        public virtual ICollection<PurchaseRequestDetails> PurchaseRequestDetails { get; set; }
        public virtual ICollection<SellRequest> SellRequests{ get; set; }
        public virtual ICollection<SellRequestDetails> SellRequestDetails { get; set; }
        public virtual ICollection<StoreItems> StoreItems { get; set; }
        public virtual ICollection<Suppliers> Suppliers { get; set; }
        public virtual ICollection<SuppliersPayments> SuppliersPayments { get; set; }

    }
}
