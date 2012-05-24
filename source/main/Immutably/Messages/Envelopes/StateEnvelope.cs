namespace Immutably.Messages
{
    public class StateEnvelope : IStateEnvelope
    {
        public IState State { get; private set; }
        public IStateMetadata Metadata { get; private set; }

        public StateEnvelope(IState state, IStateMetadata metadata)
        {
            State = state;
            Metadata = metadata;
        }
    }
}