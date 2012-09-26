using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Commands;
using Calendar.Messages.Events;
using NServiceBus;

namespace Calendar.MessageHandlers.CommandHandlers
{
    public class MakeBookingHandler : IHandleMessages<MakeBooking>
    {
        private readonly IBus _bus;
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBookingTypeRepository _bookingTypeRepository;

        public MakeBookingHandler(
            IBus bus,
            IBookingRepository bookingRepository,
            IEmployeeRepository employeeRepository,
            IBookingTypeRepository bookingTypeRepository)
        {
            _bus = bus;
            _bookingRepository = bookingRepository;
            _employeeRepository = employeeRepository;
            _bookingTypeRepository = bookingTypeRepository;
            DomainEvents.Register<BookingMadeEvent>(BookingMade);
            DomainEvents.Register<MakeBookingInvalidatedEvent>(MakeBookingInvalidated);
        }

        public void Handle(MakeBooking command)
        {
            var employee = _employeeRepository.GetById(command.EmployeeId);
            var bookingType = _bookingTypeRepository.GetById(command.BookingTypeId);
            Booking.Make(command.Id, employee, command.Start, command.End, bookingType);
        }

        public void BookingMade(BookingMadeEvent @event)
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

        public void MakeBookingInvalidated(MakeBookingInvalidatedEvent @event)
        {
            var makeBookingInvalidated = new MakeBookingInvalidated
            {
                Id = @event.Source,
                Message = @event.ValidationMessages.FullMessage
            };

            _bus.Publish(makeBookingInvalidated);
        }
    }
}
