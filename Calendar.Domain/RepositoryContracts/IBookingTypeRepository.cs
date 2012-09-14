using System;
using Calendar.Domain.Common;
using Calendar.Domain.Entities;

namespace Calendar.Domain.RepositoryContracts
{
    public interface IBookingTypeRepository : IRepository<BookingType, Guid>
    {
    }
}
