﻿using System;

namespace Sales.Application.Requests
{
    public class UpdateTimeAllocationRequest
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
