using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Lombiq.Hosting.DistributedEvents.Services;
using Orchard.Environment.Extensions;
using Orchard.WebApi.Filters;

namespace Lombiq.Hosting.DistributedEvents.Filters
{
    // This filter can cause a stack overflow without actually doing anything because of a suspected .NET
    // framework bug, see: https://orchard.codeplex.com/discussions/558025
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.ForegroundPollingEventRaising")]
    public class PollingEventRaisingApiFilter : ActionFilterAttribute, IApiFilterProvider
    {
        private readonly IForegroundPolledEventRaiser _polledEventRaiser;


        public PollingEventRaisingApiFilter(IForegroundPolledEventRaiser polledEventRaiser)
        {
            _polledEventRaiser = polledEventRaiser;
        }


        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _polledEventRaiser.TryRaise(Constants.TimeSpanBetweenForegroundPolls);
        }
    }
}