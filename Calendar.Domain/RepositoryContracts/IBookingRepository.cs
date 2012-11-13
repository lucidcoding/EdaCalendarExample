using System;
using System.Collections.Generic;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.RepositoryContracts
{
    public interface IBookingRepository : IRepository<Booking, Guid>
    {
        IEnumerable<Booking> GetByEmployeeId(Guid employeeId);
        IEnumerable<Booking> Search(Guid employeeId, DateTime start, DateTime end);
    }
}
