using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.Events
{
    public class EmployeeRegisteredEvent : DomainEvent<Employee>
    {
        public EmployeeRegisteredEvent(Employee employee)
            : base(employee)
        {
        }
    }
}
