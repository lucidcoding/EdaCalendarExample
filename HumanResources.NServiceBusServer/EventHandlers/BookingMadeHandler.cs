using Calendar.Messages.Events;
using HumanResources.Domain.Global;
using NServiceBus;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;

namespace HumanResources.NServiceBusServer.EventHandlers
{
    public class BookingMadeHandler : IHandleMessages<BookingMade>
    {
        private readonly IEmployeeService _employeeService;

        public BookingMadeHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Handle(BookingMade @event)
        {
            //Don't add this if it is relevant to this system - this will already have been done locally.
            if (@event.BookingTypeId != Constants.HolidayBookingTypeId)
            {
                var request = new BookTimeAllocationRequest
                                  {
                                      TimeAllocationId = @event.Id,
                                      ConsultantId = @event.EmployeeId,
                                      Start = @event.Start,
                                      End = @event.End
                                  };

                _employeeService.BookTimeAllocation(request);
            }
        }
    }
}
