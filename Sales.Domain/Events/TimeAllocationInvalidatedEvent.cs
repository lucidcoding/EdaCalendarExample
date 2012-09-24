using Sales.Domain.Common;
using Sales.Domain.Entities;

namespace Sales.Domain.Events
{
    public class TimeAllocationInvalidatedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationInvalidatedEvent(TimeAllocation timeAllocation)
            : base(timeAllocation)
        {
        }
    }
}
