using System;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    //todo: not entirely sure if this should take the guid as class 
    //parameter, but there may be no entity yet!
    public class MakeBookingInvalidatedEvent : DomainEvent<Guid>
    {
        public ValidationMessageCollection ValidationMessages { get; set; }

        public MakeBookingInvalidatedEvent(Guid guid, ValidationMessageCollection validationMessages)
            : base(guid)
        {
            ValidationMessages = validationMessages;
        }
    }
}
