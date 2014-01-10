using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Orchard;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    /// <summary>
    /// Stores where signal processing currently is.
    /// </summary>
    public interface ISignalCursor : ISingletonDependency
    {
        /// <summary>
        /// The ID of the signal that was processed last.
        /// </summary>
        int LastSignalId { get; set; }
    }


    public class SignalCursor : ISignalCursor
    {
        private ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        private int _lastSignalId;
        public int LastSignalId
        {
            get
            {
                try
                {
                    _locker.EnterReadLock();
                    return _lastSignalId;
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
                    _lastSignalId = value;
                }
                finally
                {
                    _locker.ExitWriteLock();
                }
            }
        }
    }
}