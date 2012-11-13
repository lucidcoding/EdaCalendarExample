using System;
using HumanResources.Domain.Common;

namespace HumanResources.Domain.Entities
{
    public class Department : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
