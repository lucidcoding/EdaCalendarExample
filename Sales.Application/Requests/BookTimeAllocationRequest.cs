using System;

namespace Sales.Application.Requests
{
    public class BookTimeAllocationRequest
    {
        public Guid Id { get; set; }
        public Guid ConsultantId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
