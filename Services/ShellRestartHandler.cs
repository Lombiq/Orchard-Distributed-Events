using Lombiq.Hosting.DistributedEvents.Events;
using Lombiq.Hosting.DistributedEvents.Models;
using Orchard.Environment;
using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.Shell")]
    public class ShellRestartHandler : IDistributedShellRestartTriggerer, IDistributedEventHandler
    {
        private const string TenantRestartEventName = "Lombiq.Hosting.DistributedEvents.ShellRestart";

        private readonly IDistributedEventService _eventService;
        private readonly ShellSettings _shellSettings;
        private readonly IShellSettingsManagerEventHandler _shellSettingsEvents;
        private readonly IOrchardHost _orchardHost;


        public ShellRestartHandler(
            IDistributedEventService eventService,
            ShellSettings shellSettings,
            IShellSettingsManagerEventHandler shellSettingsEvents,
            IOrchardHost orchardHost)
        {
            _eventService = eventService;
            _shellSettings = shellSettings;
            _shellSettingsEvents = shellSettingsEvents;
            _orchardHost = orchardHost;
        }


        void IDistributedShellRestartTriggerer.TriggerRestart(ShellSettings settings)
        {
            var context = string.Empty;
            if (settings != null) context = ShellSettingsSerializer.ComposeSettings(settings);
            _eventService.Trigger(TenantRestartEventName, context);
        }

        void IDistributedEventHandler.Triggered(IDistributedEvent distributedEvent)
        {
        }

        void IDistributedEventHandler.Raised(IDistributedEvent distributedEvent)
        {
            if (distributedEvent.Name != TenantRestartEventName) return;

            var shellSettings = !string.IsNullOrEmpty(distributedEvent.Context) ? ShellSettingsSerializer.ParseSettings(distributedEvent.Context) : _shellSettings;
            shellSettings["IsShellRestart"] = "True";
            _shellSettingsEvents.Saved(shellSettings);
            _orchardHost.EndRequest();
        }
    }
}