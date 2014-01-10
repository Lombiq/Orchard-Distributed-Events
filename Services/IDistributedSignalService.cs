using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchard;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    public interface IDistributedSignalService : IDependency
    {
        /// <summary>
        /// Triggers a distributed event. The event will be raised on other server nodes (but not this one).
        /// </summary>
        /// <param name="signalName"></param>
        /// <param name="context"></param>
        void Trigger(string signalName, string context);


        void TryRaise();
    }


    public static class DistributedSignalServiceExtensions
    {
        public static void Trigger(this IDistributedSignalService signalService, string signalName)
        {
            signalService.Trigger(signalName, null);
        }
    }
}
