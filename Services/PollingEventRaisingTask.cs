using Lombiq.Hosting.DistributedEvents.Services;
using Orchard.Environment.Extensions;
using Orchard.Tasks;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.PollingEventRaising")]
    public class PollingEventRaisingTask : IBackgroundTask
    {
        private readonly IDistributedEventService _eventService;


        public PollingEventRaisingTask(IDistributedEventService eventService)
        {
            _eventService = eventService;
        }
        
		
        public void Sweep()
        {
            _eventService.TryRaise();
        }
    }
}