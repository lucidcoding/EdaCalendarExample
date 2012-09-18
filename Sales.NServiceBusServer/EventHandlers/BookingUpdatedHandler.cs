using Calendar.Messages.Events;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Global;

namespace Sales.NServiceBusServer.EventHandlers
{
    public class BookingUpdatedHandler : IHandleMessages<BookingUpdated>
    {
        private readonly ITimeAllocationService _timeAllocationService;

        public BookingUpdatedHandler(ITimeAllocationService timeAllocationService)
        {
            _timeAllocationService = timeAllocationService;
        }

        public void Handle(BookingUpdated @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.SalesAppointmentBookingTypeId)
            {
                var request = new UpdateTimeAllocationRequest
                {
                    Id = @event.Id,
                    Start = @event.Start,
                    End = @event.End
                };

                _timeAllocationService.Update(request);
            }
        }
    }
}
