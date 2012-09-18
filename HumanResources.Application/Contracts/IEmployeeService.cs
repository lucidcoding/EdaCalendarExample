using System;
using System.Collections.Generic;
using HumanResources.Application.DataTransferObjects;

namespace HumanResources.Application.Contracts
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll();
        EmployeeDto GetById(Guid id);
    }
}
