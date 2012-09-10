using System;

namespace HumanResources.Application.DataTransferObjects
{
    public class TimeAllocationDto
    {
        public Guid? Id { get; set; }
        public EmployeeDto Employee { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
