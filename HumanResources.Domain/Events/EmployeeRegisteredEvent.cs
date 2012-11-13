using HumanResources.Domain.Common;
using HumanResources.Domain.Entities;

namespace HumanResources.Domain.Events
{
    public class EmployeeRegisteredEvent : DomainEvent<Employee>
    {
        public EmployeeRegisteredEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
