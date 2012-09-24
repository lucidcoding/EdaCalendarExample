using System.Linq;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Commands;
using Calendar.Messages.Events;
using NServiceBus;

namespace Calendar.MessageHandlers.CommandHandlers
{
    public class UpdateBookingHandler : IHandleMessages<UpdateBooking>
    {
        private readonly IBus _bus;
        private readonly IBookingRepository _bookingRepository;

        public UpdateBookingHandler(
            IBus bus, 
            IBookingRepository bookingRepository)
        {
            _bus = bus;
            _bookingRepository = bookingRepository;
            DomainEvents.Register<BookingUpdatedEvent>(BookingUpdated);
            DomainEvents.Register<UpdateBookingInvalidatedEvent>(UpdateBookingInvalidated);
        }

        public void Handle(UpdateBooking command)
        {
            var booking = _bookingRepository.GetById(command.Id);
            booking.Update(command.Start, command.End);
        }

        public void BookingUpdated(BookingUpdatedEvent @event)
        {
            _bookingRepository.Save(@event.Source);

            var bookingUpdated = new BookingUpdated
            {
                Id = @event.Source.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End,
                BookingTypeId = @event.Source.BookingType.Id.Value
            };

            _bus.Publish(bookingUpdated);
        }

        public void UpdateBookingInvalidated(UpdateBookingInvalidatedEvent @event)
        {
            var makeBookingInvalidated = new UpdateBookingInvalidated
            {
                Id = @event.Source.Id.Value,
                Message = @event.ValidationMessages.FullMessage
            };

            _bus.Publish(makeBookingInvalidated);
        }
    }
}
