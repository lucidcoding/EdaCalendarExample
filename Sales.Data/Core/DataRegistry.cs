using NHibernate;
using Sales.Data.Common;
using Sales.Data.Repositories;
using Sales.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Sales.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IConsultantRepository>().Use<ConsultantRepository>();
                For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(
                    SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}