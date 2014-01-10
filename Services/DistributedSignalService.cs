using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lombiq.Hosting.DistributedSignals.Events;
using Lombiq.Hosting.DistributedSignals.Models;
using Orchard;
using Orchard.Data;
using Orchard.Environment;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    public class DistributedSignalService : IDistributedSignalService, IOrchardShellEvents
    {
        private readonly IDistributedSignalEventHandler _eventHandler;
        private readonly IRepository<DistributedSignalRecord> _repository;
        private readonly ISignalCursor _signalCursor;


        public DistributedSignalService(
            IDistributedSignalEventHandler eventHandler,
            IRepository<DistributedSignalRecord> repository,
            ISignalCursor signalCursor)
        {
            _eventHandler = eventHandler;
            _repository = repository;
            _signalCursor = signalCursor;
        }
        
		
        public void Trigger(string signalName, string context)
        {
            if (string.IsNullOrEmpty(signalName)) throw new ArgumentNullException("signalName");
            if (signalName.Length > 255) throw new ArgumentException("The signal name should be less than 255 characters long.");

            _repository.Create(new DistributedSignalRecord { Name = signalName, Context = context });
            _eventHandler.Triggered(new DistributedSignal { Name = signalName, Context = context });
        }

        void IOrchardShellEvents.Activated()
        {
            var lastRecord = _repository.Table.OrderByDescending(record => record.Id).FirstOrDefault();
            if (lastRecord == null) return;
            _signalCursor.LastSignalId = lastRecord.Id;
        }

        void IOrchardShellEvents.Terminating()
        {
        }


        private class DistributedSignal : IDistributedSignal
        {
            public string Name { get; set; }
            public string Context { get; set; }
        }
    }
}