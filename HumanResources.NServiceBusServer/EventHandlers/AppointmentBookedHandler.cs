using Sales.Messages.Events;
using NServiceBus;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;

namespace HumanResources.NServiceBusServer.EventHandlers
{
    public class AppointmentBookedHandler : IHandleMessages<AppointmentBooked>
    {
        private readonly IEmployeeService _employeeService;

        public AppointmentBookedHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public void Handle(AppointmentBooked @event)
        {
            var request = new BookTimeAllocationRequest
            {
                TimeAllocationId = @event.AppointmentId,
                ConsultantId = @event.EmployeeId,
                Start = @event.Date + @event.StartTime,
                End = @event.Date + @event.EndTime
            };

            _employeeService.BookTimeAllocation(request);
        }
    }
}
