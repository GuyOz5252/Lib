using Lib.Events.Abstract;

namespace Lib.Events;

public class DataEvent<T> : IEvent
{
    public T Data { get; set; }
}