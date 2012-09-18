using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class TimeAllocationUpdatedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationUpdatedEvent(TimeAllocation timeAllocation)
            : base(timeAllocation)
        {
        }
    }
}
