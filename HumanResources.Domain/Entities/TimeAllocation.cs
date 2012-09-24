using System;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class TimeAllocation : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual bool Invalidated { get; set; }
        public virtual string InvalidatedMessage { get; set; }

        public static void Book(Employee employee, Guid timeAllocationId, DateTime start, DateTime end)
        {
            var timeAllocation = new TimeAllocation
            {
                Id = timeAllocationId,
                Employee = employee,
                Start = start,
                End = end
            };

            employee.TimeAllocations.Add(timeAllocation);
            DomainEvents.Raise(new TimeAllocationBookedEvent(timeAllocation));
        }

        public virtual void Update(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
            DomainEvents.Raise(new TimeAllocationUpdatedEvent(this));
        }

        public virtual void Invalidate(string message)
        {
            Invalidated = true;
            InvalidatedMessage = message;
            DomainEvents.Raise(new TimeAllocationInvalidatedEvent(this));
        }
    }
}
