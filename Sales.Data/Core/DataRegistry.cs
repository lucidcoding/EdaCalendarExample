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
                For<IAppointmentRepository>().Use<AppointmentRepository>();
                For<ITimeAllocationRepository>().Use<TimeAllocationRepository>();
                For<ISessionProvider>().Use<SessionProvider>();
                For<ISessionFactory>().Singleton().Use(SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}