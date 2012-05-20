namespace Immutably.Messages
{
    public interface ICommandEnvelope<TId> : IMessageEnvelope
    {
        ICommand Command { get; }
        new ICommandMetadata<TId> Metadata { get; }
    }
}