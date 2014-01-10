using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lombiq.Hosting.DistributedEvents.Models;
using Orchard.Events;

namespace Lombiq.Hosting.DistributedEvents.Events
{
    /// <summary>
    /// Event handler for distributed event events.
    /// </summary>
    public interface IDistributedEventHandler : IEventHandler
    {
        /// <summary>
        /// Fired when a distributed event is triggered; it's fired only on the node where the event is triggered.
        /// </summary>
        /// <param name="event">The event that was triggered.</param>
        void Triggered(IDistributedEvent distributedEvent);

        /// <summary>
        /// Fired when a distributed event is noticed; it's fired only on the nodes that are consuming the event (i.e. all the nodes
        /// where the event was NOT raised).
        /// </summary>
        /// <param name="event">The event that was raised.</param>
        void Raised(IDistributedEvent distributedEvent);
    }
}
