using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class TimeAllocationBookedEvent : DomainEvent<TimeAllocation>
    {
        public TimeAllocationBookedEvent(TimeAllocation timeAllocation) : base(timeAllocation)
        {
        }
    }
}
