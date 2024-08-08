namespace BuildinBlocks.Core.Messages;

public abstract class Message<TAgreggateType>
    {
        public string MessageType { get; protected set; }
        public TAgreggateType? AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
