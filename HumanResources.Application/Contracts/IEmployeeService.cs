using System;
using System.Collections.Generic;
using HumanResources.Domain.Entities;

namespace HumanResources.Application.Contracts
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee GetById(Guid id);
    }
}
