using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Builds statefull aggregate context
    /// </summary>
    public class StatefullAggregateContextBuilder<TState>
    {
        private TState _aggregateState;
        private String _aggregateId = null;
        private IDataFactory _dataFactory;
        private Int32 _aggregateVersion = 0;
        private Action<TState> _stateBuilder = null;

        public StatefullAggregateContextBuilder<TState> SetId(String id)
        {
            _aggregateId = id;
            return this;
        }

        public StatefullAggregateContextBuilder<TState> SetVersion(Int32 version)
        {
            _aggregateVersion = version;
            return this;
        }

        public StatefullAggregateContextBuilder<TState> SetState(TState state)
        {
            _aggregateState = state;
            return this;
        }

        public StatefullAggregateContextBuilder<TState> SetState(Action<TState> state)
        {
            _stateBuilder = state;
            return this;
        }

        public StatefullAggregateContextBuilder<TState> SetDataFactory(IDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
            return this;
        }

        public IStatefullAggregateContext Build()
        {
            if (_aggregateState == null)
            {
                if (_dataFactory != null)
                    _aggregateState = _dataFactory.Create<TState>();
                else
                    _aggregateState = Activator.CreateInstance<TState>();

                if (_stateBuilder != null)
                    _stateBuilder(_aggregateState);
            }

            if (_aggregateId == null)
                _aggregateId = Guid.NewGuid().ToString();

            return new StatefullAggregateContext(_aggregateState, _aggregateId, _aggregateVersion, _dataFactory);
        }

    }
}