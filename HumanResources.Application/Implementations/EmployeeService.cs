using System;
using System.Collections.Generic;
using AutoMapper;
using HumanResources.Application.Contracts;
using HumanResources.Application.DataTransferObjects;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.Events;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Context;
using NServiceBus;

namespace HumanResources.Application.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            ISessionFactory sessionFactory, 
            IEmployeeRepository employeeRepository)
        {
            _sessionFactory = sessionFactory;
            _employeeRepository = employeeRepository;
        }

        public List<EmployeeDto> GetAll()
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                List<Employee> employees;

                using (var transaction = session.BeginTransaction())
                {
                    employees = _employeeRepository.GetAll();
                    transaction.Commit();
                }

                Mapper.Reset();
                Mapper.CreateMap<Employee, EmployeeDto>()
                    .ForMember(prop => prop.TimeAllocations, opt => opt.Ignore());

                var employeeDtos = Mapper.Map<List<Employee>, List<EmployeeDto>>(employees);
                return employeeDtos;
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public EmployeeDto GetById(Guid id)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                Employee employee;

                using (var transaction = session.BeginTransaction())
                {
                    employee = _employeeRepository.GetById(id);
                    transaction.Commit();
                }

                Mapper.Reset();
                Mapper.CreateMap<Employee, EmployeeDto>();
                Mapper.CreateMap<TimeAllocation, TimeAllocationDto>()
                    .ForMember(prop => prop.Employee, opt => opt.Ignore())
                    .Include<Holiday, HolidayDto>();
                Mapper.CreateMap<Holiday, HolidayDto>();
                var employeeDto = Mapper.Map<Employee, EmployeeDto>(employee);
                return employeeDto;
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public ValidationMessageCollection ValidateBookHoliday(BookHolidayRequest request)
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

                return employee.ValidateBookHoliday(request.Start, request.End);
            }
            finally
            {
                CurrentSessionContext.Unbind(_sessionFactory);
                session.Dispose();
            }
        }

        public void BookHoliday(BookHolidayRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var employee = _employeeRepository.GetById(request.EmployeeId);
                    DomainEvents.Register<HolidayBookedEvent>(HolidayBooked);
                    employee.BookHoliday(request.Id, request.Start, request.End);
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
            _employeeRepository.SaveOrUpdate(@event.Source.Employee);
        }

        public void BookTimeAllocation(BookTimeAllocationRequest request)
        {
            var session = _sessionFactory.OpenSession();
            CurrentSessionContext.Bind(session);

            try
            {
                using (var transaction = session.BeginTransaction())
                {
                    var consultant = _employeeRepository.GetById(request.ConsultantId);
                    DomainEvents.Register<TimeAllocationBookedEvent>(TimeAllocationBooked);
                    consultant.BookTimeAllocation(request.TimeAllocationId, request.Start, request.End);
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

        private void TimeAllocationBooked(TimeAllocationBookedEvent @event)
        {
            _employeeRepository.SaveOrUpdate(@event.Source.Employee);
        }
    }
}
