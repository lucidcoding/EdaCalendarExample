using Calendar.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
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
            if (timeAllocation != null) timeAllocation.Invalidate(@event.Message);
        }

        private void TimeAllocationInvalidated(TimeAllocationInvalidatedEvent @event)
        {
            _timeAllocationRepository.Save(@event.Source);
        }
    }
}
