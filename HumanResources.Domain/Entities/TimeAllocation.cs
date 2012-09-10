using System;
using HumanResources.Domain.Common;

namespace HumanResources.Domain.Entities
{
    public class TimeAllocation : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
    }
}
