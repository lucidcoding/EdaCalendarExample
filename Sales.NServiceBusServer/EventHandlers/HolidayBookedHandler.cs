using HumanResources.Messages.Events;
using NServiceBus;
using Sales.Application.Contracts;
using Sales.Application.Requests;

namespace Sales.NServiceBusServer.EventHandlers
{
    public class HolidayBookedHandler : IHandleMessages<HolidayBooked>
    {
        private readonly IConsultantService _consultantService;

        public HolidayBookedHandler(IConsultantService consultantService)
        {
            _consultantService = consultantService;
        }

        public void Handle(HolidayBooked @event)
        {
            var request = new BookTimeAllocationRequest
            {
                TimeAllocationId = @event.HolidayId,
                ConsultantId = @event.EmployeeId,
                Start = @event.Start,
                End = @event.End
            };

            _consultantService.BookTimeAllocation(request);
        }
    }
}
