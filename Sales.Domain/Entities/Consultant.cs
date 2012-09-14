using System;
using System.Collections.Generic;
using System.Linq;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Consultant : Entity<Guid>
    {
        public virtual string Forename { get; set; }
        public virtual string Surname { get; set; }
        public virtual IList<TimeAllocation> TimeAllocations { get; set; }

        public virtual string FullName
        {
            get { return Forename + " " + Surname; }
        }

        public virtual IList<Appointment> Appointments
        {
            get
            {
                return (from timeAllocation in TimeAllocations
                        where (timeAllocation as Appointment) != null
                        select timeAllocation as Appointment)
                    .ToList();
            }
        }

        public virtual ValidationMessageCollection ValidateBookAppointment(
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {

            var validationMessages = new ValidationMessageCollection();
            if (date == default(DateTime)) validationMessages.AddError("Date", "Date not correctly set.");
            if (date.TimeOfDay != default(TimeSpan)) validationMessages.AddError("Date", "Time part of date should not be set.");
            if (startTime == default(TimeSpan)) validationMessages.AddError("StartTime", "Start time not correctly set.");
            if (endTime == default(TimeSpan)) validationMessages.AddError("EndTime", "End time not correctly set.");
            if (string.IsNullOrEmpty(leadName)) validationMessages.AddError("LeadName", "Lead name not correctly set.");
            if (string.IsNullOrEmpty(address)) validationMessages.AddError("Address", "Address not correctly set.");
            if (!validationMessages.IsValid) return validationMessages;

            if (date.Year != 2012)
                validationMessages.AddError("Appointments can only be booked for 2012."); //For simplicity in this example.

            if (startTime > endTime)
                validationMessages.AddError("Start date is after end date.");

            if (!validationMessages.IsValid) return validationMessages;

            //This validation is common to both domains.
            var start = date + startTime;
            var end = date + endTime;

            var matchingTimeAllocations = (from timeAllocation in TimeAllocations
                                           where (start >= timeAllocation.Start && start <= timeAllocation.End)
                                              || (end >= timeAllocation.Start && end <= timeAllocation.End)
                                              || (start <= timeAllocation.Start && end >= timeAllocation.End)
                                           select timeAllocation)
                .ToList();

            if (matchingTimeAllocations.Any())
                validationMessages.AddError("Appointment clashes with other time allocations for employee.");

            //This logic is exclusive to this domain.
            var visitsToLeadInlastMonth = (from appointment in Appointments
                                           where appointment.Start > start.AddMonths(-1)
                                                 && appointment.LeadName == leadName
                                           select appointment)
                .ToList();

            if (visitsToLeadInlastMonth.Any())
                validationMessages.AddError("Lead has already had a visit in the last month.");

            return validationMessages;
        }

        public virtual void BookAppointment(
            Guid id,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {
            var validationMessages = ValidateBookAppointment(date, startTime, endTime, leadName, address);
            if (validationMessages.Count > 0) throw new ValidationException(validationMessages);

            var start = date + startTime;
            var end = date + endTime;

            var appointment = new Appointment
            {
                Id = id,
                Consultant = this,
                Start = start,
                End = end,
                LeadName = leadName,
                Address = address
            };

            TimeAllocations.Add(appointment);
            DomainEvents.Raise(new AppointmentBookedEvent(appointment));
        }

        public virtual void BookTimeAllocation(Guid timeAllocationId, DateTime start, DateTime end)
        {
            var timeAllocation = new TimeAllocation()
            {
                Id = timeAllocationId,
                Consultant = this,
                Start = start,
                End = end
            };

            TimeAllocations.Add(timeAllocation);
            DomainEvents.Raise(new TimeAllocationBookedEvent(timeAllocation));
        }
    }
}
