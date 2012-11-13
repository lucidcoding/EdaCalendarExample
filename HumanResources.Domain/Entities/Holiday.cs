using System;
using System.Linq;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;

namespace HumanResources.Domain.Entities
{
    public class Holiday : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual bool Approved { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual bool Invalidated { get; set; }
        public virtual string InvalidatedMessage { get; set; }
        
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

            //todo: three employees from same department cannot be off at same time?

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

            employee.Holidays.Add(holiday);
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
