using System.Threading;
using Orchard;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    /// <summary>
    /// Stores where event processing currently is.
    /// </summary>
    public interface IEventCursor : ISingletonDependency
    {
        /// <summary>
        /// The ID of the event that was processed last.
        /// </summary>
        int LastEventId { get; set; }
    }


    public class EventCursor : IEventCursor
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private int _lastEventId;
        public int LastEventId
        {
            get
            {
                _lock.EnterReadLock();
                try
                {
                    return _lastEventId;
                }
                finally
                {
                    _lock.ExitReadLock();
                }
            }

            set
            {
                _lock.EnterWriteLock();
                try
                {
                    _lastEventId = value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }
    }
}