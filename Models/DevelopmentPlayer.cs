using System;
using System.Collections.Generic;

namespace Software.Models
{
    public partial class DevelopmentPlayer
    {
        public uint PlayerId { get; set; }
        public uint? ProductId { get; set; }
        public uint? EmployeeId { get; set; }
        public string? EmployeeRole { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual SoftwareProduct? Product { get; set; }
    }
}
