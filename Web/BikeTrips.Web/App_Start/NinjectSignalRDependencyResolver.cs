using Microsoft.AspNet.SignalR;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BikeTrips.Web.App_Start
{
    public class NinjectSignalRDependencyResolver: DefaultDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectSignalRDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public override object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType).Concat(base.GetServices(serviceType));
        }
    }
}