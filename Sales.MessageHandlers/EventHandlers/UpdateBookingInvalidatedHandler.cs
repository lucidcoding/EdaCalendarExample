using Calendar.Messages.Events;
using NServiceBus;
using Sales.Domain.Common;
using Sales.Domain.Events;
using Sales.Domain.RepositoryContracts;

namespace Sales.MessageHandlers.EventHandlers
{
    public class UpdateBookingInvalidatedHandler : IHandleMessages<UpdateBookingInvalidated>
    {
        private readonly ITimeAllocationRepository _timeAllocationRepository;

        public UpdateBookingInvalidatedHandler(ITimeAllocationRepository timeAllocationRepository)
        {
            _timeAllocationRepository = timeAllocationRepository;
            DomainEvents.Register<TimeAllocationInvalidatedEvent>(TimeAllocationInvalidated);
        }

        public void Handle(UpdateBookingInvalidated @event)
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
