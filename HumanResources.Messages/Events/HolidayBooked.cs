using System;
using NServiceBus;

namespace HumanResources.Messages.Events
{
    public class HolidayBooked : IEvent
    {
        public Guid HolidayId { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
