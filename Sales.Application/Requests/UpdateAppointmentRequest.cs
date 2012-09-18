using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.Application.Requests
{
    public class UpdateAppointmentRequest
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string LeadName { get; set; }
        public string Address { get; set; }
    }
}
