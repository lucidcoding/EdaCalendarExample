using System;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate;

namespace Calendar.Data.Repositories
{
    public class BookingTypeRepository : Repository<BookingType, Guid>, IBookingTypeRepository
    {
        public BookingTypeRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
