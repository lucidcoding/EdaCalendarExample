using System;
using MasterData.Data.Common;
using MasterData.Domain.Entities;
using MasterData.Domain.RepositoryContracts;
using NHibernate;

namespace MasterData.Data.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
