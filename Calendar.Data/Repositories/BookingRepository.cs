using System;
using System.Collections.Generic;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate;
using NHibernate.Criterion;

namespace Calendar.Data.Repositories
{
    public class BookingRepository : Repository<Booking, Guid>, IBookingRepository
    {
        public BookingRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }

        public IEnumerable<Booking> GetByEmployeeId(Guid employeeId)
        {
            return _sessionFactory
                .GetCurrentSession()
                .CreateCriteria<Booking>()
                .Add(Restrictions.Eq("EmployeeId", employeeId))
                .List<Booking>();
        }

        public IEnumerable<Booking> Search(Guid employeeId, DateTime start, DateTime end)
        {
            return _sessionFactory
                .GetCurrentSession()
                .CreateCriteria<Booking>()
                .Add(Restrictions.Eq("EmployeeId", employeeId))
                .List<Booking>();
        }
    }
}
