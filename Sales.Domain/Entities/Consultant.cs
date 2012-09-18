using System;
using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Common;

namespace Sales.Domain.Entities
{
    public class Consultant : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<TimeAllocation> TimeAllocations { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public virtual IList<Appointment> Appointments
        {
            get
            {
                return (from timeAllocation in TimeAllocations
                        where (timeAllocation as Appointment) != null
                        select timeAllocation as Appointment)
                    .ToList();
            }
        }
    }
}
