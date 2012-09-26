using System;
using System.Collections.Generic;
using System.Linq;
using Calendar.Domain.Common;
using Calendar.Domain.Events;

namespace Calendar.Domain.Entities
{
    public class Booking : Entity<Guid>
    {
        public virtual Employee Employee { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual BookingType BookingType { get; set; }
        //public virtual bool Invalidated { get; set; }
        //public virtual string InvalidatedMessage { get; set; }

        //Udi always says validation should be done in UI, and domain should just throw unhelful error because if it gets that
        //then it is likely to be a hacker - but how do you account for validation logic like this? Surely it's not the end of
        //the world that any system that adds bookings into a calendar should detect clashes?
        private static ValidationMessageCollection ValidateMakeUpdate(DateTime start, DateTime end, IEnumerable<Booking> bookings, Booking bookingBeingUpdated)
        {
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

            var matchingTimeAllocations = (from booking in bookings
                                           where (bookingBeingUpdated == null || bookingBeingUpdated != booking)
                                              && ((start >= booking.Start && start <= booking.End)
                                              || (end >= booking.Start && end <= booking.End)
                                              || (start <= booking.Start && end >= booking.End))
                                           select booking)
               .ToList();

            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Booking clashes with other bookings for employee.");

            return validationMessages;
        }

        public static ValidationMessageCollection ValidateMake(Employee employee, DateTime start, DateTime end)
        {
            return ValidateMakeUpdate(start, end, employee.Bookings, null);
        }

        public static Booking Make(Guid bookingId, Employee employee, DateTime start, DateTime end, BookingType type)
        {
            var validationMessages = ValidateMake(employee, start, end);

            if (validationMessages.Count > 0)
            {
                DomainEvents.Raise(new MakeBookingInvalidatedEvent(bookingId, validationMessages));
                return null;
            }

            var booking = new Booking
            {
                Id = bookingId,
                Employee = employee,
                Start = start,
                End = end,
                BookingType = type
            };

            employee.Bookings.Add(booking);
            DomainEvents.Raise(new BookingMadeEvent(booking));
            return booking;
        }

        public virtual ValidationMessageCollection ValidateUpdate(DateTime start, DateTime end)
        {
            return ValidateMakeUpdate(start, end, Employee.Bookings, this);
        }

        public virtual void Update(DateTime start, DateTime end)
        {
            var validationMessages = ValidateUpdate(start, end);

            if (validationMessages.Count > 0)
                DomainEvents.Raise(new UpdateBookingInvalidatedEvent(this, validationMessages));

            Start = start;
            End = end;
            DomainEvents.Raise(new BookingUpdatedEvent(this));
        }
    }
}
