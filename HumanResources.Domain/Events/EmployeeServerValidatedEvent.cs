using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeServerValidatedEvent : DomainEvent<Employee>
    {
        public EmployeeServerValidatedEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
