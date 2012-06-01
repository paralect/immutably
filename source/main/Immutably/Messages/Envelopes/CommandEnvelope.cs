namespace Immutably.Messages
{
    public class CommandEnvelope : MessageEnvelope, ICommandEnvelope
    {
        public new ICommandMetadata Metadata
        {
            get { return (ICommandMetadata) base.Metadata; }
        }

        public object Command
        {
            get { return Message; }
        }

        public CommandEnvelope(object message, ICommandMetadata metadata) : base(message, metadata)
        {
        }
    }
}