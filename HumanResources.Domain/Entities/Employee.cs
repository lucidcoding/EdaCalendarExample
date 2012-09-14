using System;
using System.Collections.Generic;
using System.Linq;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Employee : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime? Joined { get; set; }
        public virtual DateTime? Left { get; set; }
        public virtual int HolidayEntitlement { get; set; }
        public virtual IList<TimeAllocation> TimeAllocations { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public virtual IList<Holiday> Holidays
        {
            get
            {
                return (from timeAllocation in TimeAllocations
                        where (timeAllocation as Holiday) != null
                        select timeAllocation as Holiday)
                    .ToList();
            }
        }

        public virtual int RemainingHoliday
        {
            get { return HolidayEntitlement - Holidays.Sum(x => x.TotalDays); }
        }

        public virtual ValidationMessageCollection ValidateBookHoliday(DateTime start, DateTime end)
        {
            var validationMessages = new ValidationMessageCollection();

            if(start == default(DateTime) || end == default(DateTime))
                validationMessages.AddError("Start and end time not correctly set.");
                
            if(!validationMessages.IsValid)
                return validationMessages;

            if(start.Year != 2012 || end.Year != 2012)
                validationMessages.AddError("Holidays can only be booked for 2012."); //For simplicity in this example.

            if(start > end)
                validationMessages.AddError("Start date is after end date.");

            if (!validationMessages.IsValid)
                return validationMessages;

            //This validation is common to both domains.
            var matchingTimeAllocations = (from timeAllocation in TimeAllocations
                                           where (start >= timeAllocation.Start && start <= timeAllocation.End)
                                              || (end >= timeAllocation.Start && end <= timeAllocation.End)
                                              || (start <= timeAllocation.Start && end >= timeAllocation.End)
                                           select timeAllocation)
                .ToList();

            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Holiday clashes with other time allocations for employee.");

            //This logic is exclusive to this domain.
            if((end - start).Days > RemainingHoliday)
                validationMessages.AddError("Employee does not have enough remaining holiday.");

            return validationMessages;
        }

        public virtual void BookHoliday(Guid id, DateTime start, DateTime end)
        {
            var validationMessages = ValidateBookHoliday(start, end);
            if(validationMessages.Count > 0) throw new ValidationException(validationMessages);

            var holiday = new Holiday
                              {
                                  Id = id,
                                  Employee = this,
                                  Approved = true,
                                  Start = start,
                                  End = end
                              };

            TimeAllocations.Add(holiday);
            DomainEvents.Raise(new HolidayBookedEvent(holiday));
        }

        public virtual void BookTimeAllocation(Guid timeAllocationId, DateTime start, DateTime end)
        {
            var timeAllocation = new TimeAllocation
            {
                Id = timeAllocationId,
                Employee = this,
                Start = start,
                End = end
            };

            TimeAllocations.Add(timeAllocation);
            DomainEvents.Raise(new TimeAllocationBookedEvent(timeAllocation));
        }
    }
}
