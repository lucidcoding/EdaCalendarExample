using System;
using Calendar.Domain.Common;

namespace Calendar.Domain.Entities
{
    public class BookingType : Entity<Guid>
    {
        public virtual string Description { get; set; }
    }
}
