using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    /// <summary>
    /// Raises events when polling happens in the foreground.
    /// </summary>
    public interface IForegroundPolledEventRaiser : IDependency
    {
        /// <summary>
        /// Tries to raise events if the given time ellapsed since the last time the method was called.
        /// </summary>
        /// <param name="ellapsed">Time span to check whether it already ellapsed since the last time the method was called.</param>
        void TryRaise(TimeSpan ellapsed);
    }


    [OrchardFeature("Lombiq.Hosting.DistributedEvents.ForegroundPollingEventRaising")]
    public class ForegroundPolledEventRaiser : IForegroundPolledEventRaiser
    {
        private readonly ILastForegroundPollTimeAccessor _lastPollTimeAccessor;
        private readonly IClock _clock;
        private readonly IDistributedEventService _eventService;


        public ForegroundPolledEventRaiser(
            ILastForegroundPollTimeAccessor lastPollTimeAccessor,
            IClock clock,
            IDistributedEventService eventService)
        {
            _lastPollTimeAccessor = lastPollTimeAccessor;
            _clock = clock;
            _eventService = eventService;
        }


        public void TryRaise(TimeSpan ellapsed)
        {
            if (_lastPollTimeAccessor.LastPollDateTimeUtc.Add(ellapsed) > _clock.UtcNow) return;

            _lastPollTimeAccessor.Update();
            _eventService.TryRaise();
        }
    }
}