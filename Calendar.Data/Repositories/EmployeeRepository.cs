using System;
using Calendar.Data.Common;
using Calendar.Domain.Entities;
using Calendar.Domain.RepositoryContracts;
using NHibernate;

namespace Calendar.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
