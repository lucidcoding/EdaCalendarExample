using System;
using System.Linq;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Holiday : TimeAllocation
    {
        public virtual bool Approved { get; set; }
        
        public virtual int TotalDays
        {
            get { return (End.Date.AddDays(1) - Start.Date).Days; }
        }

        private static ValidationMessageCollection ValidateBookUpdate(
            Employee employee,
            DateTime start, 
            DateTime end, 
            Holiday holidayBeingUpdated)
        {
            var startDateTime = start.Date + new TimeSpan(0, 9, 0, 0);
            var endDateTime = end + new TimeSpan(0, 17, 0, 0);

            var validationMessages = new ValidationMessageCollection();

            if (startDateTime == default(DateTime) || endDateTime == default(DateTime))
                validationMessages.AddError("Start and end time not correctly set.");

            if (!validationMessages.IsValid)
                return validationMessages;

            if (startDateTime.Year != 2012 || endDateTime.Year != 2012)
                validationMessages.AddError("Holidays can only be booked for 2012."); //For simplicity in this example.

            if (startDateTime > end)
                validationMessages.AddError("Start date is after end date.");

            if (!validationMessages.IsValid)
                return validationMessages;

            var matchingTimeAllocations = (from timeAllocation in employee.TimeAllocations
                                           where (holidayBeingUpdated == null || holidayBeingUpdated != timeAllocation)
                                              && ((startDateTime >= timeAllocation.Start && startDateTime <= timeAllocation.End)
                                              || (endDateTime >= timeAllocation.Start && endDateTime <= timeAllocation.End)
                                              || (startDateTime <= timeAllocation.Start && endDateTime >= timeAllocation.End))
                                           select timeAllocation)
               .ToList();

            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Holiday clashes with other bookings for employee.");

            if((endDateTime - startDateTime).Days > employee.RemainingHoliday)
                validationMessages.AddError("Employee does not have enough remaining holiday.");

            return validationMessages;
        }

        public static ValidationMessageCollection ValidateBook(Employee employee, DateTime start, DateTime end)
        {
            return ValidateBookUpdate(employee, start, end, null);
        }

        public static void Book(Guid id, Employee employee, DateTime start, DateTime end)
        {
            var startDateTime = start.Date + new TimeSpan(0, 9, 0, 0);
            var endDateTime = end + new TimeSpan(0, 17, 0, 0);
            var validationMessages = ValidateBook(employee, startDateTime, endDateTime);
            if(validationMessages.Count > 0) throw new ValidationException(validationMessages);

            var holiday = new Holiday
                              {
                                  Id = id,
                                  Employee = employee,
                                  Approved = true,
                                  Start = startDateTime,
                                  End = endDateTime
                              };

            employee.TimeAllocations.Add(holiday);
            DomainEvents.Raise(new HolidayBookedEvent(holiday));
        }

        public virtual ValidationMessageCollection ValidateUpdate(DateTime start, DateTime end)
        {
            return ValidateBookUpdate(this.Employee, start, end, this);
        }

        public virtual new void Update(DateTime start, DateTime end)
        {
            var startDateTime = start.Date + new TimeSpan(0, 9, 0, 0);
            var endDateTime = end + new TimeSpan(0, 17, 0, 0);
            var validationMessages = ValidateUpdate(startDateTime, endDateTime);
            if (validationMessages.Count > 0) throw new ValidationException(validationMessages);
            Start = start;
            End = end;
            DomainEvents.Raise(new HolidayUpdatedEvent(this));
        }
    }
}
