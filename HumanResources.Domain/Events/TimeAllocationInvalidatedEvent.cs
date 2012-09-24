using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class TimeAllocationInvalidatedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationInvalidatedEvent(TimeAllocation timeAllocation)
            : base(timeAllocation)
        {
        }
    }
}
