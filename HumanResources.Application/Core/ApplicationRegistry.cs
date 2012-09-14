using HumanResources.Application.Contracts;
using HumanResources.Application.Implementations;
using HumanResources.Data.Core;
using StructureMap.Configuration.DSL;

namespace HumanResources.Application.Core
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
