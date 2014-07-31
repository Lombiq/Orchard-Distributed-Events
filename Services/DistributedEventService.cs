using System;
using System.Linq;
using Lombiq.Hosting.DistributedEvents.Events;
using Lombiq.Hosting.DistributedEvents.Models;
using Lombiq.Hosting.DistributedEvents.Services;
using Orchard.Caching.Services;
using Orchard.Data;
using Orchard.Environment;
using Orchard.Services;
using Orchard.Tasks.Scheduling;
using Piedone.HelpfulLibraries.Tasks;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    public class DistributedEventService : IDistributedEventService, IOrchardShellEvents, IScheduledTaskHandler
    {
        private const string TaskType = "Lombiq.Hosting.DistributedEvents.DistributedEventService";
        private const string CacheKey = "Lombiq.Hosting.DistributedEvents.EventRecords";

        private readonly IDistributedEventHandler _eventHandler;
        private readonly IRepository<DistributedEventRecord> _repository;
        private readonly IEventCursor _eventCursor;
        private readonly IEnvironmentIdentifierProvider _environmentIdentifierProvider;
        private readonly IScheduledTaskManager _scheduledTaskManager;
        private readonly ICacheService _cacheService;
        private readonly IClock _clock;


        public DistributedEventService(
            IDistributedEventHandler eventHandler,
            IRepository<DistributedEventRecord> repository,
            IEventCursor eventCursor,
            IEnvironmentIdentifierProvider environmentIdentifierProvider,
            IScheduledTaskManager scheduledTaskManager,
            ICacheService cacheService,
            IClock clock)
        {
            _eventHandler = eventHandler;
            _repository = repository;
            _eventCursor = eventCursor;
            _environmentIdentifierProvider = environmentIdentifierProvider;
            _scheduledTaskManager = scheduledTaskManager;
            _cacheService = cacheService;
            _clock = clock;
        }


        public void Trigger(string eventName, string context)
        {
            if (string.IsNullOrEmpty(eventName)) throw new ArgumentNullException("eventName");
            if (eventName.Length > 255) throw new ArgumentException("The event name should be less than 255 characters long.");

            _repository.Create(new DistributedEventRecord { Name = eventName, EnvironmentIdentifier = _environmentIdentifierProvider.GetIdentifier(), Context = context });
            _eventHandler.Triggered(new DistributedEvent { Name = eventName, Context = context });
        }

        public void TryRaise(bool force = false)
        {
            var environmentIdentifier = _environmentIdentifierProvider.GetIdentifier();

            if (force)
                _cacheService.Remove(CacheKey);

            var newEvents = _cacheService.Get(
                CacheKey,
                () => _repository.Table
                    .Where(record => record.Id > _eventCursor.LastEventId && record.EnvironmentIdentifier != environmentIdentifier)
                    .OrderBy(record => record.Id),
                TimeSpan.FromMinutes(1));

            foreach (var distributedEvent in newEvents)
            {
                _eventHandler.Raised(new DistributedEvent { Name = distributedEvent.Name, Context = distributedEvent.Context });
                _eventCursor.LastEventId = distributedEvent.Id;
            }
        }

        void IOrchardShellEvents.Activated()
        {
            Renew(false);

            var lastRecord = _repository.Table.OrderByDescending(record => record.Id).FirstOrDefault();
            if (lastRecord == null) return;
            _eventCursor.LastEventId = lastRecord.Id;
        }

        void IOrchardShellEvents.Terminating()
        {
        }

        public void Process(ScheduledTaskContext context)
        {
            if (context.Task.TaskType != TaskType) return;

            Renew(true);

            // Clean up old events
            var cleanUpCount = _repository.Table.Count() - 1000;
            if (cleanUpCount <= 0) return;
            foreach (var record in _repository.Table.OrderBy(record => record.Id).Take(cleanUpCount))
            {
                _repository.Delete(record);
            }
        }


        private void Renew(bool calledFromTaskProcess)
        {
            _scheduledTaskManager.CreateTaskIfNew(TaskType, _clock.UtcNow.AddMinutes(5), null, calledFromTaskProcess);
        }


        private class DistributedEvent : IDistributedEvent
        {
            public string Name { get; set; }
            public string Context { get; set; }
        }
    }
}