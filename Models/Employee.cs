using System;
using System.Collections.Generic;

namespace Software.Models
{
    public partial class Employee
    {
        public Employee()
        {
            DevelopmentPlayers = new HashSet<DevelopmentPlayer>();
        }

        public uint EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateOnly HireDate { get; set; }
        public uint Salary { get; set; }

        public virtual ICollection<DevelopmentPlayer> DevelopmentPlayers { get; set; }
    }
}
