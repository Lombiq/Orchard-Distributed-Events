using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Orchard;
using Orchard.Environment.Extensions;
using Orchard.Services;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    public interface ILastForegroundPollTimeAccessor : ISingletonDependency
    {
        DateTime LastPollDateTimeUtc { get; }
        void Update();
    }


    [OrchardFeature("Lombiq.Hosting.DistributedEvents.ForegroundPollingEventRaising")]
    public class LastForegroundPollTimeAccessor : ILastForegroundPollTimeAccessor
    {
        private readonly IClock _clock;

        private long _lastPollDateTimeUtcTicks;
        public DateTime LastPollDateTimeUtc
        {
            get
            {
                // Reading the ticks atomically
                return new DateTime(Interlocked.CompareExchange(ref _lastPollDateTimeUtcTicks, 0, 0));
            }

            private set
            {
                // Atomic write for the ticks
                Interlocked.Exchange(ref _lastPollDateTimeUtcTicks, value.Ticks);
            }
        }


        public LastForegroundPollTimeAccessor(IClock clock)
        {
            _clock = clock;
            LastPollDateTimeUtc = clock.UtcNow;
        }


        public void Update()
        {
            LastPollDateTimeUtc = _clock.UtcNow;
        }
    }
}