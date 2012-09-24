using System;
using NServiceBus;

namespace Calendar.Messages.Events
{
    public class UpdateBookingInvalidated : IEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
