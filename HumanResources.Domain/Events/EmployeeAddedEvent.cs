using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeAddedEvent : DomainEvent<Employee>
    {
        public EmployeeAddedEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
