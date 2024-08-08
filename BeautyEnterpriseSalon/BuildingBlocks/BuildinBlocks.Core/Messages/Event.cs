using MediatR;

namespace BuildinBlocks.Core.Messages;

public class Event<TAggregateType> : Message<TAggregateType>, INotification
{
    public DateTime Timestamp { get; private set; }

    protected Event()
    {
        Timestamp = DateTime.Now.ToUniversalTime().AddHours(-3);
    }
}