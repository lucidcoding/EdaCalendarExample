using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sales.UI.Common
{

    public class NServiceBusControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}