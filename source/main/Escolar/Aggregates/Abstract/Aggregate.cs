using Paralect.Machine.Processes;

namespace Escolar.Aggregates
{
    public class Aggregate<TState> : IAggregate
    {
        /// <summary>
        /// User state
        /// </summary>
        protected TState _state;

        public void Initialize(IState state)
        {
            _state = (TState) state;
        }
    }
}