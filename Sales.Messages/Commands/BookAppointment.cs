using System;

namespace Sales.Messages.Commands
{
    public class BookAppointment
    {
        public Guid ConsultantId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string LeadName { get; set; }
        public string Address { get; set; }
    }
}
