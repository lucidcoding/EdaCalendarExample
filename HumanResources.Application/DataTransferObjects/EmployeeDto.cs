using System;
using System.Collections.Generic;

namespace HumanResources.Application.DataTransferObjects
{
    public class EmployeeDto
    {
        public Guid? Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public DateTime? Joined { get; set; }
        public DateTime? Left { get; set; }
        public int HolidayEntitlement { get; set; }
        public IList<TimeAllocationDto> TimeAllocations { get; set; }
        public int RemainingHoliday { get; set; }
        public string FullName { get; set; }
    }
}
