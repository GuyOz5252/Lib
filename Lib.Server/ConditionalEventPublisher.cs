using Lib.Events.Abstract;
using Lib.Server.Abstract;

namespace Lib.Server;

public class ConditionalEventPublisher : IEventPublisher
{
    private readonly Action<IEvent> _condition;

    public ConditionalEventPublisher(Action<IEvent> condition)
    {
        _condition = condition;
    }

    public void Publish(IEvent @event)
    {
        _condition(@event);
    }
}