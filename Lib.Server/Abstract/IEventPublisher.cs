using Lib.Events.Abstract;

namespace Lib.Server.Abstract;

public interface IEventPublisher
{
    void Publish(IEvent @event);
}