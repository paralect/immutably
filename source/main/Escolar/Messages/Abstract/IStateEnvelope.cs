namespace Paralect.Machine.Processes
{
    public interface IStateEnvelope<TId>
    {
        IState State { get; }
        IStateMetadata<TId> Metadata { get; }
    }

}