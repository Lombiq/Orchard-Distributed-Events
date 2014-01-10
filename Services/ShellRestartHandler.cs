using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lombiq.Hosting.DistributedSignals.Events;
using Lombiq.Hosting.DistributedSignals.Models;
using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    [OrchardFeature("Lombiq.Hosting.DistributedSignals.ShellLifetime")]
    public class ShellRestartHandler : IDistributedShellRestartTriggerer, IDistributedSignalEventHandler
    {
        private const string TenantRestartSignalName = "ShellRestart";

        private readonly IDistributedSignalService _signalService;
        private readonly ShellSettings _shellSettings;
        private readonly IShellSettingsManagerEventHandler _shellSettingsEvents;


        public ShellRestartHandler(
            IDistributedSignalService signalService,
            ShellSettings shellSettings,
            IShellSettingsManagerEventHandler shellSettingsEvents)
        {
            _signalService = signalService;
            _shellSettings = shellSettings;
            _shellSettingsEvents = shellSettingsEvents;
        }


        void IDistributedShellRestartTriggerer.TriggerRestart(ShellSettings settings)
        {
            var context = string.Empty;
            if (settings != null) context = ShellSettingsSerializer.ComposeSettings(settings);
            _signalService.Trigger(TenantRestartSignalName, context);
        }

        void IDistributedSignalEventHandler.Triggered(IDistributedSignal signal)
        {
        }

        void IDistributedSignalEventHandler.Raised(IDistributedSignal signal)
        {
            if (signal.Name != TenantRestartSignalName) return;

            var shellSettings = !string.IsNullOrEmpty(signal.Context) ? ShellSettingsSerializer.ParseSettings(signal.Context) : _shellSettings;
            _shellSettingsEvents.Saved(shellSettings);
        }
    }
}