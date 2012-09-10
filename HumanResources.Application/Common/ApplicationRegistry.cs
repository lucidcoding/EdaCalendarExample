using HumanResources.Application.Contracts;
using HumanResources.Application.Implementations;
using HumanResources.Data.Common;
using StructureMap.Configuration.DSL;

namespace HumanResources.Application.Common
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            Configure(x =>
            {
                For<IEmployeeService>().Use<EmployeeService>();
                x.ImportRegistry(typeof(DataRegistry));
            });
        }
    }
}
