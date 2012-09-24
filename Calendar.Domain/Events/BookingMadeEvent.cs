using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class BookingMadeEvent : DomainEvent<Booking>
    {
        public BookingMadeEvent(Booking booking)
            : base(booking)
        {
        }
    }
}
