using System;
using System.Collections.Generic;
using AutoMapper;
using HumanResources.Application.Contracts;
using HumanResources.Application.DataTransferObjects;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Context;

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
    }
}
