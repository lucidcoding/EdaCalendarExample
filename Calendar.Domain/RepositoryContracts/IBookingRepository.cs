using System;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.RepositoryContracts
{
    public interface IBookingRepository : IRepository<Booking, Guid>
    {
    }
}
