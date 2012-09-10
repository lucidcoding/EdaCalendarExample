using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class TimeAllocationBookedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationBookedEvent(TimeAllocation timeAllocation) : base(timeAllocation)
        {
        }
    }
}
