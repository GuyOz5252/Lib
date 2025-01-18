using Lib.Events;
using Lib.Server;

namespace Lib;

public class Program
{
    public static void Main(string[] args)
    {
        var @event = new DataEvent<string>()
        {
            Data = "Hello"
        };
        
        var componentA = new Component()
        {
            ComponentName = "Component A"
        };

        var componentB = new Component()
        {
            ComponentName = "Component B"
        };

        var conditionalSubscription = new ConditionalSubscriptionFactory<string>()
        {
            PropertyName = "Data",
            PublishIfTrue = new List<Component> { componentA },
            PublishIfFalse = new List<Component> { componentB },
            ParameterType = typeof(DataEvent<string>),
            EqualsTo = "Hello"
        }.Create();

        conditionalSubscription.Publish(@event);
    }
}