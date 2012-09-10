using System;

namespace Sales.Application.DataTransferObjects
{
    public class TimeAllocationDto
    {
        public Guid? Id { get; set; }
        public ConsultantDto Consultant { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
