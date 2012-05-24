namespace Immutably.Messages
{
    public class CommandEnvelope : MessageEnvelope, ICommandEnvelope
    {
        public new ICommandMetadata Metadata
        {
            get { return (ICommandMetadata) base.Metadata; }
        }

        public ICommand Command
        {
            get { return (ICommand) Message; }
        }

        public CommandEnvelope(ICommand message, ICommandMetadata metadata) : base(message, metadata)
        {
        }
    }
}