using Orchard;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    /// <summary>
    /// Service for dealing with distributed events; that is, events that can travel across nodes in a multi-node environment. Thus this
    /// service broadcasts events from this server node to other nodes.
    /// </summary>
    public interface IDistributedEventService : IDependency
    {
        /// <summary>
        /// Triggers a distributed event. The event will be raised on other server nodes (but not this one).
        /// </summary>
        /// <param name="eventName">Name of the event to identify it.</param>
        /// <param name="context">Optional context to provide for consumers.</param>
        void Trigger(string eventName, string context);

        /// <summary>
        /// Raises new events that were triggered from other nodes, if there are any.
        /// </summary>
        void TryRaise();
    }


    public static class DistributedEventServiceExtensions
    {
        /// <summary>
        /// Triggers a distributed event. The event will be raised on other server nodes (but not this one).
        /// </summary>
        /// <param name="eventName">Name of the event to identify it.</param>
        public static void Trigger(this IDistributedEventService eventService, string eventName)
        {
            eventService.Trigger(eventName, null);
        }
    }
}