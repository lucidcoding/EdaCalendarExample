using Calendar.Messages.Events;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using NServiceBus;

namespace Sales.NServiceBusServer.EventHandlers
{
    public class UpdateBookingInvalidatedHandler : IHandleMessages<UpdateBookingInvalidated>
    {
        private readonly ITimeAllocationService _timeAllocationService;

        public UpdateBookingInvalidatedHandler(ITimeAllocationService timeAllocationService)
        {
            _timeAllocationService = timeAllocationService;
        }

        public void Handle(UpdateBookingInvalidated @event)
        {
            var request = new InvalidateTimeAllocationRequest
            {
                Id = @event.Id,
                Message = @event.Message
            };

            _timeAllocationService.Invalidate(request);
        }
    }
}
