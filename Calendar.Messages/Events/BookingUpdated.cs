using System;
using NServiceBus;

namespace Calendar.Messages.Events
{
    public class BookingUpdated : IEvent
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid BookingTypeId { get; set; }
    }
}
