using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class UpdateBookingInvalidatedEvent : DomainEvent<Booking>
    {
        public ValidationMessageCollection ValidationMessages { get; set; }

        public UpdateBookingInvalidatedEvent(Booking booking, ValidationMessageCollection validationMessages)
            : base(booking)
        {
            ValidationMessages = validationMessages;
        }
    }
}
