using System;
using NHibernate;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class TimeAllocationRepository : Repository<TimeAllocation, Guid>, ITimeAllocationRepository
    {
        public TimeAllocationRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
