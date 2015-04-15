using Orchard.Environment.Configuration;
using Orchard.Environment.Extensions;

namespace Lombiq.Hosting.DistributedEvents.Services
{
    // This would work great, if it would always work... See: https://github.com/OrchardCMS/Orchard/issues/4242

    /// <summary>
    /// Triggers a shell restart on all the server nodes if the shell settings change.
    /// </summary>
    [OrchardFeature("Lombiq.Hosting.DistributedEvents.Shell")]
    public class ShellSettingsShellRestartTriggerer : IShellSettingsManagerEventHandler
    {
        private readonly IDistributedShellRestartTriggerer _restartTriggerer;


        public ShellSettingsShellRestartTriggerer(IDistributedShellRestartTriggerer restartTriggerer)
        {
            _restartTriggerer = restartTriggerer;
        }


        public void Saved(ShellSettings settings)
        {
            // If this Saved() event was raised to restart the shell then let's not raise another restart event.
            if (settings["IsShellRestart"] == "True")
            {
                settings["IsShellRestart"] = "False";
                return;
            }

            _restartTriggerer.TriggerRestart(settings);
        }
    }
}