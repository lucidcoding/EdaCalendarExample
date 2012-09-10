using System;

namespace Sales.Domain.Entities
{
    public class Appointment : TimeAllocation
    {
        public virtual string LeadName { get; set; }
        public virtual string Address { get; set; }

        public virtual DateTime Date
        {
            get { return Start.Date; }
        }

        public virtual TimeSpan StartTime
        {
            get { return Start.TimeOfDay; }
        }

        public virtual TimeSpan EndTime
        {
            get { return End.TimeOfDay; }
        }
    }
}
