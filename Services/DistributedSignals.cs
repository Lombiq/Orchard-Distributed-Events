using Lombiq.Hosting.DistributedEvents.Events;
using Lombiq.Hosting.DistributedEvents.Models;
using Orchard.Caching;
using Orchard.Environment;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.Signals")]
    [OrchardSuppressDependency("Orchard.Caching.Signals")]
    public class DistributedSignals : Signals, ISignals, IDistributedEventHandler
    {
        private const string EventName = "Lombiq.Hosting.DistributedSignal";

        private readonly Work<IDistributedEventService> _eventServiceWork;
        private readonly IJsonConverter _jsonConverter;


        public DistributedSignals(Work<IDistributedEventService> eventServiceWork, IJsonConverter jsonConverter)
        {
            _eventServiceWork = eventServiceWork;
            _jsonConverter = jsonConverter;
        }
        
		
        void ISignals.Trigger<T>(T signal)
        {
            var stringified = Stringify(signal);
            _eventServiceWork.Value.Trigger(EventName, stringified);
            base.Trigger(stringified);
        }

        IVolatileToken ISignals.When<T>(T signal)
        {
            return base.When(Stringify(signal));
        }

        void IDistributedEventHandler.Triggered(IDistributedEvent distributedEvent)
        {
        }

        void IDistributedEventHandler.Raised(IDistributedEvent distributedEvent)
        {
            if (distributedEvent.Name != EventName) return;

            base.Trigger(distributedEvent.Context);
        }


        private string Stringify<T>(T signal)
        {
            if (signal is string) return signal.ToString();

            return _jsonConverter.Serialize(signal); // This is to achieve that the string should only depend on the object's content.
        }
    }
}