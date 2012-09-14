using System;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate;

namespace Calendar.Data.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
