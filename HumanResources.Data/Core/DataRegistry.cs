using HumanResources.Data.Common;
using HumanResources.Data.Repositories;
using HumanResources.Domain.RepositoryContracts;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace HumanResources.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IEmployeeRepository>().Use<EmployeeRepository>();
                For<IHolidayRepository>().Use<HolidayRepository>();
                For<ISessionProvider>().Use<SessionProvider>();
                For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}