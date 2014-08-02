using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lombiq.Hosting.DistributedEvents
{
    public static class Constants
    {
        public static readonly TimeSpan TimeSpanBetweenForegroundPolls = new TimeSpan(0, 0, 15);
    }
}