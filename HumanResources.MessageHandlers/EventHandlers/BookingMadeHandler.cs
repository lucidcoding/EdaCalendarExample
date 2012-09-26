using Calendar.Messages.Events;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Global;
using HumanResources.Domain.RepositoryContracts;
using NServiceBus;

namespace HumanResources.MessageHandlers.EventHandlers
{
    public class BookingMadeHandler : IHandleMessages<BookingMade>
    {
        private readonly ITimeAllocationRepository _timeAllocationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookingMadeHandler(
            ITimeAllocationRepository timeAllocationRepository,
            IEmployeeRepository employeeRepository)
        {
            _timeAllocationRepository = timeAllocationRepository;
            _employeeRepository = employeeRepository;
            DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
        }

        public void Handle(BookingMade @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.HolidayBookingTypeId)
            {
                var employee = _employeeRepository.GetById(@event.EmployeeId);
                TimeAllocation.Book(employee, @event.Id, @event.Start, @event.End);
            }
        }

        private void TimeAllocationBooked(TimeAllocationBookedEvent @event)
        {
            _timeAllocationRepository.Save(@event.Source);
        }
    }
}
