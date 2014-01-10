
namespace Lombiq.Hosting.DistributedEvents.Models
{
    public interface IDistributedEvent
    {
        string Name { get; }
        string Context { get; }
    }
}
