using NHibernate;
using MasterData.Data.Common;
using MasterData.Data.Repositories;
using MasterData.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace MasterData.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IEmployeeRepository>().Use<EmployeeRepository>();
                For<IDepartmentRepository>().Use<DepartmentRepository>();
                For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(
                    SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}