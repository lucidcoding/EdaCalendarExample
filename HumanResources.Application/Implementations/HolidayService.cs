using System;
using System.Transactions;
using Calendar.Messages.Commands;
using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.Global;
using HumanResources.Domain.RepositoryContracts;
using NHibernate.Context;
using NServiceBus;

namespace HumanResources.Application.Implementations
{
    public class HolidayService : IHolidayService
    {
        private readonly IBus _bus;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public HolidayService(
            IBus bus,
            IHolidayRepository holidayRepository,
            IEmployeeRepository employeeRepository)
        {
            _bus = bus;
            _holidayRepository = holidayRepository;
            _employeeRepository = employeeRepository;
        }

        public ValidationMessageCollection ValidateBook(BookHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(request.EmployeeId);
                var validationMessages = Holiday.ValidateBook(employee, request.Start, request.End);
                transactionScope.Complete();
                return validationMessages;
            }
        }

        public void Book(BookHolidayRequest request)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(request.EmployeeId);
                DomainEvents.Register<HolidayBookedEvent>(HolidayBooked);
                Holiday.Book(request.Id, employee, request.Start, request.End);
                _employeeRepository.Flush();
                transactionScope.Complete();
            }
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
        }

        public ValidationMessageCollection ValidateUpdate(UpdateHolidayRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Holiday holiday;

                using (var transaction = session.BeginTransaction())
                {
                    holiday = _holidayRepository.GetById(request.Id);
                    transaction.Commit();
                }

                return holiday.ValidateUpdate(request.Start, request.End);
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public void Update(UpdateHolidayRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transactionScope = new TransactionScope())
                {
                    var holiday = _holidayRepository.GetById(request.Id);
                    DomainEvents.Register<HolidayUpdatedEvent>(HolidayUpdated);
                    holiday.Update(request.Start, request.End);
                    session.Flush();
                    transactionScope.Complete();
                }
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                DomainEvents.ClearCallbacks();
                session.Dispose();
            }
        }

        private void HolidayUpdated(HolidayUpdatedEvent @event)
        {
            _holidayRepository.Update(@event.Source);

            var updateBookingCommand = new UpdateBooking
            {
                Id = @event.Source.Id.Value,
                Start = @event.Source.Start,
                End = @event.Source.End
            };

            _bus.Send(updateBookingCommand);
        }
    }
}
