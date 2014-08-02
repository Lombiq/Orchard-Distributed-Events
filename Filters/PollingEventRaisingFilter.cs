using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lombiq.Hosting.DistributedEvents.Services;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Filters;

namespace Lombiq.Hosting.DistributedEvents.Filters
{
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.PollingEventRaising")]
    public class PollingEventRaisingFilter : FilterProvider, IResultFilter
    {
        private readonly IDistributedEventService _eventService;

        public PollingEventRaisingFilter(IDistributedEventService eventService)
        {
            _eventService = eventService;			
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _eventService.TryRaise();
        }
    }
}