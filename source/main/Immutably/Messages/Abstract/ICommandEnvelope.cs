namespace Immutably.Messages
{
    public interface ICommandEnvelope : IMessageEnvelope
    {
        ICommand Command { get; }
        new ICommandMetadata Metadata { get; }
    }
}