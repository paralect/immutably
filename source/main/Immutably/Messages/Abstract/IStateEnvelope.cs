namespace Immutably.Messages
{
    public interface IStateEnvelope
    {
        IState State { get; }
        IStateMetadata Metadata { get; }
    }

}