using Lib.Events.Abstract;
using Lib.Server.Abstract;

namespace Lib.Server;

public class Component
{
    public string ComponentName { get; set; }
    
    public IEventPublisher EventPublisher { get; set; }
    
    public void InjectEvent(IEvent @event)
    {
        ProcessEvent(@event);
    }

    private void ProcessEvent(IEvent @event)
    {
        var processedEvent = Processor(@event);
        EventPublisher?.Publish(processedEvent);
    }

    private IEvent Processor(IEvent @event)
    {
        Console.WriteLine($"{ComponentName} processing event");
        return @event;
    }
}