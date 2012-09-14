using Calendar.Data.Common;
using Calendar.Data.Repositories;
using Calendar.Domain.RepositoryContracts;
using NHibernate;
using StructureMap.Configuration.DSL;

namespace Calendar.Data.Core
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            Configure(x =>
            {
                For<IBookingRepository>().Use<BookingRepository>();
                For<IBookingTypeRepository>().Use<BookingTypeRepository>();
                For<IEmployeeRepository>().Use<EmployeeRepository>();
                For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(
                    SessionFactoryFactory.GetSessionFactory());
            });
        }
    }
}