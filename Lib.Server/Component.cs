using Lib.Events.Abstract;

namespace Lib.Server;

public class Component
{
    public string ComponentName { get; set; }
    
    public void Enque(IEvent @event)
    {
        Console.WriteLine($"{ComponentName}: Enque");
    }
}