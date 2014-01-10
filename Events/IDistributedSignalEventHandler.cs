using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lombiq.Hosting.DistributedSignals.Models;
using Orchard.Events;

namespace Lombiq.Hosting.DistributedSignals.Events
{
    /// <summary>
    /// Event handler for distributed signal events.
    /// </summary>
    public interface IDistributedSignalEventHandler : IEventHandler
    {
        /// <summary>
        /// Fired when a distributed signal is triggered; it's fired on the node where the signal is triggered.
        /// </summary>
        /// <param name="signal">The signal that was triggered.</param>
        void Triggered(IDistributedSignal signal);

        /// <summary>
        /// Fired when a distributed signal is noticed; it's fired on the nodes that are only consuming the signal (i.e. all the nodes
        /// where the signal was NOT raised).
        /// </summary>
        /// <param name="signal">The signal that was raised.</param>
        void Raised(IDistributedSignal signal);
    }
}
