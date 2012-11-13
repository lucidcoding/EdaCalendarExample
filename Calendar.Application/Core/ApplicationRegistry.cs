using Calendar.Data.Core;
using StructureMap.Configuration.DSL;

namespace Calendar.Application.Core
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                //For<IAppointmentService>().Use<AppointmentService>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}

