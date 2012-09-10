using System;
using System.Collections.Generic;
using HumanResources.Application.DataTransferObjects;
using HumanResources.Application.Requests;
using HumanResources.Domain.Common;

namespace HumanResources.Application.Contracts
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetAll();
        EmployeeDto GetById(Guid id);
        ValidationMessageCollection ValidateBookHoliday(BookHolidayRequest request);
        void BookHoliday(BookHolidayRequest request);
        void BookTimeAllocation(BookTimeAllocationRequest request);
    }
}
