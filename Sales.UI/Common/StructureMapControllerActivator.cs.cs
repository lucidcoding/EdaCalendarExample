using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace Sales.UI.Common
{
    public class StructureMapControllerActivator : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType != null)
                {
                    return ObjectFactory.GetInstance(controllerType) as Controller;
                }
                return null;

            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.WhatDoIHave());
                throw;
            }
        }
    }
}