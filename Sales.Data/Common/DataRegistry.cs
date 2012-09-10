using NHibernate;
using Sales.Data.Repositories;
using Sales.Domain.RepositoryContracts;
using StructureMap.Configuration.DSL;

namespace Sales.Data.Common
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