using System;

namespace HumanResources.Application.Requests
{
    public class BookHolidayRequest
    {
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
