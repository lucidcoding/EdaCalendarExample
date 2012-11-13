using System;

namespace Calendar.Application.Requests
{
    public class SearchBookingsRequest
    {
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
