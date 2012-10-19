using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MasterData.Domain.Common;
using MasterData.Domain.Entities;

namespace MasterData.Domain.Events
{
    public class EmployeeRegisteredEvent : DomainEvent<Employee>
    {
        public EmployeeRegisteredEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
