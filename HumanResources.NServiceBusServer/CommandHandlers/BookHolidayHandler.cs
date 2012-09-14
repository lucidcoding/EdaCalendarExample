using System;
using Calendar.Messages.Commands;
using NServiceBus;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Messages.Commands;

namespace HumanResources.NServiceBusServer.CommandHandlers
{
    public class BookHolidayHandler : IHandleMessages<BookHoliday>
    {
        private readonly IBus _bus;
        private readonly IEmployeeService _employeeService;

        public BookHolidayHandler(IBus bus, IEmployeeService employeeService)
        {
            _bus = bus;
            _employeeService = employeeService;
        }

        public void Handle(BookHoliday command)
        {
            var request = new BookHolidayRequest
            {
                Id = command.Id,
                EmployeeId = command.EmployeeId,
                Start = command.Start,
                End = command.End
            };

            _employeeService.BookHoliday(request);
            _bus.Return(0);
        }
    }
}
