using System;
using System.Collections.Generic;
using NServiceBus;

namespace Calendar.Messages.Events
{
    public class MakeBookingInvalidated : IEvent
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
