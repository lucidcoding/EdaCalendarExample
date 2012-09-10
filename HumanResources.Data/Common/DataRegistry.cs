using HumanResources.Data.Repositories;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace HumanResources.Data.Common
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IEmployeeRepository>().Use<EmployeeRepository>();
                For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(
                    SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}