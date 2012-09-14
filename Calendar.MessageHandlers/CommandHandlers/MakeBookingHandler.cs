using Calendar.Domain.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.Events;
using Calendar.Domain.RepositoryContracts;
using Calendar.Messages.Commands;
using NServiceBus;

namespace Calendar.MessageHandlers.CommandHandlers
{
    public class MakeBookingHandler : IHandleMessages<MakeBooking>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IBookingTypeRepository _bookingTypeRepository;
        private readonly IDomainEventHandler<BookingMadeEvent> _bookingMadeEventHandler;

        public MakeBookingHandler(
            IEmployeeRepository employeeRepository, 
            IBookingTypeRepository bookingTypeRepository,
            IDomainEventHandler<BookingMadeEvent> bookingMadeEventHandler)
        {
            _employeeRepository = employeeRepository;
            _bookingTypeRepository = bookingTypeRepository;
            _bookingMadeEventHandler = bookingMadeEventHandler;
            DomainEvents.Register<BookingMadeEvent>(_bookingMadeEventHandler.Handle);
        }

        public void Handle(MakeBooking command)
        {
            var employee = _employeeRepository.GetById(command.EmployeeId);
            var bookingType = _bookingTypeRepository.GetById(command.BookingTypeId);
            Booking.Make(command.Id, employee, command.Start, command.End, bookingType);
        }
    }
}
