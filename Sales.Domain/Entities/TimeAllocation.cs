using System;
using Sales.Domain.Common;

namespace Sales.Domain.Entities
{
    public class TimeAllocation : Entity<Guid>
    {
        public virtual Consultant Consultant { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
    }
}
