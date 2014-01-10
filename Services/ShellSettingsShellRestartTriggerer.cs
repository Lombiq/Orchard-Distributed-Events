using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedSignals.Services
{
    /// <summary>
    /// Triggers a shell restart on all the server nodes if the shell settings change.
    /// </summary>
    [OrchardFeature("Lombiq.Hosting.DistributedSignals.ShellLifetime")]
    public class ShellSettingsShellRestartTriggerer : IShellSettingsManagerEventHandler
    {
        private readonly IDistributedShellRestartTriggerer _restartTriggerer;


        public ShellSettingsShellRestartTriggerer(IDistributedShellRestartTriggerer restartTriggerer)
        {
            _restartTriggerer = restartTriggerer;
        }
        
		
        public void Saved(ShellSettings settings)
        {
            _restartTriggerer.TriggerRestart(settings);
        }
    }
}