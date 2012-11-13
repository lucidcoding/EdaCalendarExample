using System;

namespace Calendar.Application.Requests
{
    public class MakeBookingRequest
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid BookingTypeId { get; set; }
    }
}
