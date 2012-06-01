namespace Immutably.Messages
{
    public class StateEnvelope : IStateEnvelope
    {
        public object State { get; private set; }
        public IStateMetadata Metadata { get; private set; }

        public StateEnvelope(object state, IStateMetadata metadata)
        {
            State = state;
            Metadata = metadata;
        }
    }
}