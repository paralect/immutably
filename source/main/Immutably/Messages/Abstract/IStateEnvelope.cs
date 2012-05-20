namespace Immutably.Messages
{
    public interface IStateEnvelope<TId>
    {
        IState State { get; }
        IStateMetadata<TId> Metadata { get; }
    }

}