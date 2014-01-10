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
        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        private int _lastEventId;
        public int LastEventId
        {
            get
            {
                try
                {
                    _locker.EnterReadLock();
                    return _lastEventId;
                }
                finally
                {
                    _locker.ExitReadLock();
                }
            }

            set
            {
                try
                {
                    _locker.EnterWriteLock();
                    _lastEventId = value;
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }
    }
}