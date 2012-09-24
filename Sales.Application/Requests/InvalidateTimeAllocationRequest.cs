using System;

namespace Sales.Application.Requests
{
    public class InvalidateTimeAllocationRequest
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}
