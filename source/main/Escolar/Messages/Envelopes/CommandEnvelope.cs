namespace Escolar.Messages
{
    public class CommandEnvelope<TId> : MessageEnvelope, ICommandEnvelope<TId>
    {
        public new ICommandMetadata<TId> Metadata
        {
            get { return (ICommandMetadata<TId>) base.Metadata; }
        }

        public ICommand Command
        {
            get { return (ICommand) Message; }
        }

        public CommandEnvelope(ICommand message, ICommandMetadata<TId> metadata) : base(message, metadata)
        {
        }
    }
}