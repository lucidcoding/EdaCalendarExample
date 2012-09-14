using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Common;

namespace Calendar.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<Booking> Bookings { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }
    }
}
