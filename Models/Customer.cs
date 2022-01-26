using System;
using System.Collections.Generic;

namespace Software.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contracts = new HashSet<Contract>();
        }

        public uint CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ContactPerson { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
