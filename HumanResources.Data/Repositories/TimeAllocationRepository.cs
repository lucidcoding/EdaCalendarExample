using System;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;

namespace HumanResources.Data.Repositories
{
    public class TimeAllocationRepository : Repository<TimeAllocation, Guid>, ITimeAllocationRepository
    {
        public TimeAllocationRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
