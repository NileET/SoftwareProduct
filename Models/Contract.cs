using System;
using System.Collections.Generic;

namespace Software.Models
{
    public partial class Contract
    {
        public uint ContractId { get; set; }
        public uint CustomerId { get; set; }
        public DateOnly ContractDate { get; set; }
        public uint Cost { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual SoftwareProduct? SoftwareProduct { get; set; }
    }
}
