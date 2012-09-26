using Calendar.Messages.Events;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using NServiceBus;

namespace HumanResources.MessageHandlers.EventHandlers
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
            var timeAllocation = _timeAllocationRepository.GetById(@event.Id);
            timeAllocation.Update(@event.Start, @event.End);
        }

        private void TimeAllocationUpdated(TimeAllocationUpdatedEvent @event)
        {
            _timeAllocationRepository.Update(@event.Source);
        }
    }
}
