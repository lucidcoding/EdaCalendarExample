using Sales.Application.Contracts;
using Sales.Application.Implementations;
using Sales.Data.Common;
using StructureMap.Configuration.DSL;

namespace Sales.Application.Common
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                For<IConsultantService>().Use<ConsultantService>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}

