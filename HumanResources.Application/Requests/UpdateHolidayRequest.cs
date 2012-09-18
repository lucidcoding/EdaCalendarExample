using System;

namespace HumanResources.Application.Requests
{
    public class UpdateHolidayRequest
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
