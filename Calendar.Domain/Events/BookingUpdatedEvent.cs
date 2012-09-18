using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class BookingUpdatedEvent : DomainEvent<Booking>
    {
        public BookingUpdatedEvent(Booking booking) : base(booking)
        {
        }
    }
}
