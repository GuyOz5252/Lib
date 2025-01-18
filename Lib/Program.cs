using Lib.Events;
using Lib.Server;

namespace Lib;

public class Program
{
    public static async Task Main(string[] args)
    {
        var @event = new DataEvent<string>
        {
            Data = "HelloE"
        };
        
        var componentA = new Component
        {
            ComponentName = "Component A",
            EventPublisher = null
        };

        var componentB = new Component
        {
            ComponentName = "Component B",
            EventPublisher = null
        };

        var conditionalSubscription = new ConditionalEventPublisherFactory<string>
        {
            ParameterType = typeof(DataEvent<string>),
            PropertyName = "Data",
            EqualsTo = "Hello",
            PublishIfTrue = new List<Component> { componentA },
            PublishIfFalse = new List<Component> { componentB },
        }.Create();

        var listenerComponent = new Component
        {
            ComponentName = "Listener Component",
            EventPublisher = conditionalSubscription
        };
        
        listenerComponent.InjectEvent(@event);
    }
}