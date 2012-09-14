using System;

namespace Calendar.Messages.Commands
{
    public class MakeBooking
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; } 
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid BookingTypeId { get; set; }
    }
}
