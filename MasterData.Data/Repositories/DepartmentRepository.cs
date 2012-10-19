using System;
using MasterData.Data.Common;
using MasterData.Domain.Entities;
using MasterData.Domain.RepositoryContracts;
using NHibernate;

namespace MasterData.Data.Repositories
{
    public class DepartmentRepository : Repository<Department, Guid>, IDepartmentRepository
    {
        public DepartmentRepository(ISessionFactory sessionFactory) :
            base(sessionFactory)
        {
        }
    }
}
