using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calendar.Messages.Commands;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Global;
using HumanResources.Domain.RepositoryContracts;
using HumanResources.Messages.Commands;
using NServiceBus;

namespace HumanResources.MessageHandlers.CommandHandlers
{
    public class BookHolidayHandler : IHandleMessages<BookHoliday>
    {
        private readonly IBus _bus;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public BookHolidayHandler(IBus bus, IHolidayRepository holidayRepository, IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _holidayRepository = holidayRepository;
            _employeeRepository = employeeRepository;
        }

        public void Handle(BookHoliday message)
        {
            var employee = _employeeRepository.GetById(message.EmployeeId);
            DomainEvents.Register<HolidayBookedEvent>(HolidayBooked);
            Holiday.Book(message.Id, employee, message.Start, message.End);
        }

        private void HolidayBooked(HolidayBookedEvent @event)
        {
            _holidayRepository.Save(@event.Source);

            var makeBookingCommand = new MakeBooking
            {
                Id = @event.Source.Id.Value,
                EmployeeId = @event.Source.Employee.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End,
                BookingTypeId = Constants.HolidayBookingTypeId
            };

            _bus.Send(makeBookingCommand);

            //throw new Exception("XXXSXX!");
        }
    }
}
