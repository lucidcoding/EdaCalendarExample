using System;
using NServiceBus;

namespace Sales.Messages.Events
{
    public class AppointmentBooked : IEvent
    {
        public Guid AppointmentId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string LeadName { get; set; }
        public string Address { get; set; }
    }
}
