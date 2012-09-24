using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class TimeAllocation : Entity<Guid>
    {
        public virtual Consultant Consultant { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual bool Invalidated { get; set; }
        public virtual string InvalidatedMessage { get; set; }

        public static void Book(Consultant consultant, Guid timeAllocationId, DateTime start, DateTime end)
        {
            var timeAllocation = new TimeAllocation
            {
                Id = timeAllocationId,
                Consultant = consultant,
                Start = start,
                End = end
            };

            consultant.TimeAllocations.Add(timeAllocation);
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
