namespace Paralect.Machine.Processes
{
    public interface IStateEnvelope
    {
        IState State { get; }
        IStateMetadata Metadata { get; }
    }
}