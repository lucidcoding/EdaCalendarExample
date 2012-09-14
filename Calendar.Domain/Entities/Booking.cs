using System;
using System.Linq;
using Calendar.Domain.Common;
using Calendar.Domain.Events;

namespace Calendar.Domain.Entities
{
    //todo: note 
    //I am treating the Booking as the aggregate root in this case because that makes more sense in this domian. In a Calendar system, a booking
    //is more central than an employee.
    public class Booking : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual BookingType BookingType { get; set; }

        public static ValidationMessageCollection ValidateMake(Employee employee, DateTime start, DateTime end)
        {
            //todo: note 
            //This now contains validation common to all domains.
            var validationMessages = new ValidationMessageCollection();

            if (start == default(DateTime) || end == default(DateTime))
                validationMessages.AddError("Start and end time not correctly set.");

            if (!validationMessages.IsValid)
                return validationMessages;

            if (start.Year != 2012 || end.Year != 2012)
                validationMessages.AddError("Holidays can only be booked for 2012."); //For simplicity in this example.

            if (start > end)
                validationMessages.AddError("Start date is after end date.");

            if (!validationMessages.IsValid)
                return validationMessages;

            var matchingTimeAllocations = (from booking in employee.Bookings
                                           where (start >= booking.Start && start <= booking.End)
                                              || (end >= booking.Start && end <= booking.End)
                                              || (start <= booking.Start && end >= booking.End)
                                           select booking)
                .ToList();

            //todo: note 
            //Udi always says validation should be done in UI, and domain should just throw unhelful error because if it gets that
            //then it is likely to be a hacker - but how do you account for validation logic like this? Surely it's not the end of
            //the world that any system that adds bookings into a calendar should detect clashes?
            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Booking clashes with other bookings for employee.");

            return validationMessages;
        }

        public static void Make(Guid bookingId, Employee employee, DateTime start, DateTime end, BookingType type)
        {
            var validationMessages = ValidateMake(employee, start, end);
            if (validationMessages.Count > 0) throw new ValidationException(validationMessages);

            var booking = new Booking()
            {
                Id = bookingId,
                Employee = employee,
                Start = start,
                End = end,
                BookingType = type
            };

            DomainEvents.Raise(new BookingMadeEvent(booking));
        }
    }
}
