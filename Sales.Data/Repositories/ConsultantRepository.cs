using System;
using NHibernate;
using Sales.Data.Common;
using Sales.Domain.Entities;
using Sales.Domain.RepositoryContracts;

namespace Sales.Data.Repositories
{
    public class ConsultantRepository : Repository<Consultant, Guid>, IConsultantRepository
    {
        public ConsultantRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
