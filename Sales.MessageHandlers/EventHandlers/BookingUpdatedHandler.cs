using Calendar.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.Global;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    public class BookingUpdatedHandler : IHandleMessages<BookingUpdated>
    {
        private readonly ITimeAllocationRepository _timeAllocationRepository;

        public BookingUpdatedHandler(ITimeAllocationRepository timeAllocationRepository)
        {
            _timeAllocationRepository = timeAllocationRepository;
            DomainEvents.Register<TimeAllocationUpdatedEvent>(TimeAllocationUpdated);
        }

        public void Handle(BookingUpdated @event)
        {
            if (@event.BookingTypeId != Constants.SalesAppointmentBookingTypeId)
            {
                var timeAllocation = _timeAllocationRepository.GetById(@event.Id);
                DomainEvents.Register<TimeAllocationUpdatedEvent>(TimeAllocationUpdated);
                timeAllocation.Update(@event.Start, @event.End);
            }
        }

        private void TimeAllocationUpdated(TimeAllocationUpdatedEvent @event)
        {
            _timeAllocationRepository.Update(@event.Source);
        }
    }
}
