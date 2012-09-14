using Calendar.Domain.Common;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Events;
using NServiceBus;

namespace Calendar.MessageHandlers.DomainEventHandlers
{
    public class BookingMadeDomainEventHandler : IDomainEventHandler<BookingMadeEvent>
    {
        private readonly IBus _bus;
        private readonly IBookingRepository _bookingRepository;

        public BookingMadeDomainEventHandler(IBus bus, IBookingRepository bookingRepository)
        {
            _bus = bus;
            _bookingRepository = bookingRepository;
        }

        public void Handle(BookingMadeEvent @event)
        {
            _bookingRepository.Save(@event.Source);

            var bookingMade = new BookingMade
            {
                Id = @event.Source.Id.Value,
                EmployeeId = @event.Source.Employee.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End,
                BookingTypeId = @event.Source.BookingType.Id.Value
            };

            _bus.Publish(bookingMade);
        }
    }
}
