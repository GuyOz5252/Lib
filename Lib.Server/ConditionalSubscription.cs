using Lib.Events.Abstract;

namespace Lib.Server;

public class ConditionalSubscription
{
    private readonly Action<IEvent> _condition;

    public ConditionalSubscription(Action<IEvent> condition)
    {
        _condition = condition;
    }

    public void Publish(IEvent @event)
    {
        _condition(@event);
    }
}