using Calendar.Messages.Events;
using HumanResources.Domain.Common;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using NServiceBus;

namespace HumanResources.MessageHandlers.EventHandlers
{
    public class MakeBookingInvalidatedHandler : IHandleMessages<MakeBookingInvalidated>
    {
        private readonly ITimeAllocationRepository _timeAllocationRepository;

        public MakeBookingInvalidatedHandler(ITimeAllocationRepository timeAllocationRepository)
        {
            _timeAllocationRepository = timeAllocationRepository;
            DomainEvents.Register<TimeAllocationInvalidatedEvent>(TimeAllocationInvalidated);
        }

        public void Handle(MakeBookingInvalidated @event)
        {
            var timeAllocation = _timeAllocationRepository.GetById(@event.Id);
            timeAllocation.Invalidate(@event.Message);
        }

        private void TimeAllocationInvalidated(TimeAllocationInvalidatedEvent @event)
        {
            _timeAllocationRepository.Save(@event.Source);
        }
    }
}
