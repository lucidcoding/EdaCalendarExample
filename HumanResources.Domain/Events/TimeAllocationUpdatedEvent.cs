using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class TimeAllocationUpdatedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationUpdatedEvent(TimeAllocation timeAllocation)
            : base(timeAllocation)
        {
        }
    }
}
