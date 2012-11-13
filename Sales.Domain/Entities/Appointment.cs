using System;
using Sales.Domain.Common;
using Sales.Domain.Events;

namespace Sales.Domain.Entities
{
    public class Appointment 
    {
        public virtual Guid ConsultantId { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }
        public virtual string LeadName { get; set; }
        public virtual string Address { get; set; }
        public virtual bool Invalidated { get; set; }
        public virtual string InvalidatedMessage { get; set; }

        public virtual DateTime Date
        {
            get { return Start.Date; }
        }

        public virtual TimeSpan StartTime
        {
            get { return Start.TimeOfDay; }
        }

        public virtual TimeSpan EndTime
        {
            get { return End.TimeOfDay; }
        }

        private static ValidationMessageCollection ValidateBookUpdate(
            Guid consultantId, 
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address,
            Appointment appointmentBeingUpdated)
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

            var start = date + startTime;
            var end = date + endTime;

            //todo: need to do this validation a different way now consultant has gone.
            //var matchingTimeAllocations = (from timeAllocation in consultant.TimeAllocations
            //                               where (appointmentBeingUpdated == null || appointmentBeingUpdated != timeAllocation)
            //                                  && ((start >= timeAllocation.Start && start <= timeAllocation.End)
            //                                  || (end >= timeAllocation.Start && end <= timeAllocation.End)
            //                                  || (start <= timeAllocation.Start && end >= timeAllocation.End))
            //                               select timeAllocation)
            //    .ToList();

            //if (matchingTimeAllocations.Any())
            //    validationMessages.AddError("Appointment clashes with other time allocations for employee.");

            //var visitsToLeadInlastMonth = (from appointment in consultant.Appointments
            //                               where (appointmentBeingUpdated == null || appointmentBeingUpdated != appointment)
            //                               && appointment.Start > start.AddMonths(-1)
            //                               && appointment.LeadName == leadName
            //                               select appointment)
            //    .ToList();

            //if (visitsToLeadInlastMonth.Any())
            //    validationMessages.AddError("Lead has already had a visit in the last month.");

            return validationMessages;
        }

        public static ValidationMessageCollection ValidateBook(
            Guid consultantId,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {
            return ValidateBookUpdate(consultantId, date, startTime, endTime, leadName, address, null);
        }

        public static void Book(
            Guid consultantId,
            Guid id,
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {
            var validationMessages = ValidateBook(consultantId, date, startTime, endTime, leadName, address);
            if (validationMessages.Count > 0) throw new ValidationException(validationMessages);
            var start = date + startTime;
            var end = date + endTime;

            var appointment = new Appointment
            {
                Id = id,
                ConsultantId = consultantId,
                Start = start,
                End = end,
                LeadName = leadName,
                Address = address
            };

            DomainEvents.Raise(new AppointmentBookedEvent(appointment));
        }

        public virtual ValidationMessageCollection ValidateUpdate(
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {
            return ValidateBookUpdate(ConsultantId, date, startTime, endTime, leadName, address, this);
        }

        public virtual new void Update(
            DateTime date,
            TimeSpan startTime,
            TimeSpan endTime,
            string leadName,
            string address)
        {
            var validationMessages = ValidateUpdate(date, startTime, endTime, leadName, address);
            if (validationMessages.Count > 0) throw new ValidationException(validationMessages);
            var start = date + startTime;
            var end = date + endTime;
            Start = start;
            End = end;
            LeadName = leadName;
            Address = address;
            DomainEvents.Raise(new AppointmentUpdatedEvent(this));
        }
    }
}
