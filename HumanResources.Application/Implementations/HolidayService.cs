using HumanResources.Application.Contracts;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Context;

namespace HumanResources.Application.Implementations
{
    public class HolidayService : IHolidayService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public HolidayService(
            ISessionFactory sessionFactory, 
            IHolidayRepository holidayRepository,
            IEmployeeRepository employeeRepository)
        {
            _sessionFactory = sessionFactory;
            _holidayRepository = holidayRepository;
            _employeeRepository = employeeRepository;
        }

        public ValidationMessageCollection ValidateBook(BookHolidayRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Employee employee;

                using (var transaction = session.BeginTransaction())
                {
                    employee = _employeeRepository.GetById(request.EmployeeId);
                    transaction.Commit();
                }

                return Holiday.ValidateBook(employee, request.Start, request.End);
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public void Book(BookHolidayRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var employee = _employeeRepository.GetById(request.EmployeeId);
                    DomainEvents.Register<HolidayBookedEvent>(HolidayBooked);
                    Holiday.Book(request.Id, employee, request.Start, request.End);
                    transaction.Commit();
                }    
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                DomainEvents.ClearCallbacks();
                session.Dispose();
            }
        }

        private void HolidayBooked(HolidayBookedEvent @event)
        {
            _holidayRepository.Save(@event.Source);
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
                using (var transaction = session.BeginTransaction())
                {
                    var holiday = _holidayRepository.GetById(request.Id);
                    DomainEvents.Register<HolidayUpdatedEvent>(HolidayUpdated);
                    holiday.Update(request.Start, request.End);
                    transaction.Commit();
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
        }
    }
}
