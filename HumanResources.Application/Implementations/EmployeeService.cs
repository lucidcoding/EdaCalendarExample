using System;
using System.Collections.Generic;
using System.Transactions;

using AutoMapper;
using HumanResources.Application.Contracts;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Context;

namespace HumanResources.Application.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(
            IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAll()
        {
            using (var transactionScope = new TransactionScope())
            {
                var employees = _employeeRepository.GetAll();
                transactionScope.Complete();
                return employees;
            }
        }

        public Employee GetById(Guid id)
        {
            using (var transactionScope = new TransactionScope())
            {
                var employee = _employeeRepository.GetById(id);
                transactionScope.Complete();
                return employee;
            }
        }
    }
}
