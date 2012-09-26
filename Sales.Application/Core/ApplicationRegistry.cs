using Sales.Application.Contracts;
using Sales.Application.Implementations;
using Sales.Data.Core;
using StructureMap.Configuration.DSL;

namespace Sales.Application.Core
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                For<IAppointmentService>().Use<AppointmentService>();
                For<IConsultantService>().Use<ConsultantService>();
                //For<ITimeAllocationService>().Use<TimeAllocationService>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}

