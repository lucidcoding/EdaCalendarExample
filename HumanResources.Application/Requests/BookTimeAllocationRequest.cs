using System;

namespace HumanResources.Application.Requests
{
    public class BookTimeAllocationRequest
    {
        public Guid TimeAllocationId { get; set; }
        public Guid ConsultantId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
