using System;
using HumanResources.Data.Common;
using HumanResources.Domain.Entities;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;

namespace HumanResources.Data.Repositories
{
    public class HolidayRepository : Repository<Holiday, Guid>, IHolidayRepository
    {
        public HolidayRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
