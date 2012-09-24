using Calendar.Messages.Events;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;

namespace Sales.NServiceBusServer.EventHandlers
{
    public class MakeBookingInvalidatedHandler : IHandleMessages<MakeBookingInvalidated>
    {
        private readonly ITimeAllocationService _timeAllocationService;

        public MakeBookingInvalidatedHandler(ITimeAllocationService timeAllocationService)
        {
            _timeAllocationService = timeAllocationService;
        }

        public void Handle(MakeBookingInvalidated @event)
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
