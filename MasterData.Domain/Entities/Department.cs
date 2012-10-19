using System;
using System.Collections.Generic;
using MasterData.Domain.Common;

namespace MasterData.Domain.Entities
{
    public class Department : Entity<Guid>
    {
        public string Description { get; set; }
        public IList<Employee> Employees { get; set; } 
    }
}
