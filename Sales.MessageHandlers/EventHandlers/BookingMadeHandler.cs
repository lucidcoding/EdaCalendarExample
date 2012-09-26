using Calendar.Messages.Events;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Common;
using Sales.Domain.Entities;
using Sales.Domain.Events;
using Sales.Domain.Global;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    public class BookingMadeHandler : IHandleMessages<BookingMade>
    {
        private readonly ITimeAllocationRepository _timeAllocationRepository;
        private readonly IConsultantRepository _consultantRepository;

        public BookingMadeHandler(
            ITimeAllocationRepository timeAllocationRepository,
            IConsultantRepository consultantRepository)
        {
            _timeAllocationRepository = timeAllocationRepository;
            _consultantRepository = consultantRepository;
            DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
        }

        public void Handle(BookingMade @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.SalesAppointmentBookingTypeId)
            {
                var consultant = _consultantRepository.GetById(@event.EmployeeId);
                TimeAllocation.Book(consultant, @event.Id, @event.Start, @event.End);
            }
        }

        private void TimeAllocationBooked(TimeAllocationBookedEvent @event)
        {
            _consultantRepository.Save(@event.Source.Consultant);
        }
    }
}
