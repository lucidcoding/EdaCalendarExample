using System;

namespace HumanResources.Messages.Commands
{
    public class BookHoliday
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
