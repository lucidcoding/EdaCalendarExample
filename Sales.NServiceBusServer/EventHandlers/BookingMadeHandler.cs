using Calendar.Messages.Events;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;
using Sales.Domain.Global;

namespace Sales.NServiceBusServer.EventHandlers
{
    public class BookingMadeHandler : IHandleMessages<BookingMade>
    {
        private readonly IConsultantService _consultantService;

        public BookingMadeHandler(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        public void Handle(BookingMade @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.SalesAppointmentBookingTypeId)
            {
                var request = new BookTimeAllocationRequest
                                  {
                                      TimeAllocationId = @event.Id,
                                      ConsultantId = @event.EmployeeId,
                                      Start = @event.Start,
                                      End = @event.End
                                  };

                _consultantService.BookTimeAllocation(request);
            }
        }
    }
}
