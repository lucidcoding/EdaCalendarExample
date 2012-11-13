using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calendar.Application.Requests;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Application.Contracts
{
    public interface IBookingService
    {
        ValidationMessageCollection ValidateMake(MakeBookingRequest request);
        IEnumerable<Booking> Search(SearchBookingsRequest request);
        Booking GetById(Guid id);
    }
}
