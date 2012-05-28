using System;
using Immutably.Data;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Builds stateless aggregate context
    /// </summary>
    public class StatelessAggregateContextBuilder
    {
        private String _aggregateId = null;
        private IDataFactory _dataFactory;
        private Int32 _aggregateVersion = 0;

        public StatelessAggregateContextBuilder SetId(String id)
        {
            _aggregateId = id;
            return this;
        }

        public StatelessAggregateContextBuilder SetVersion(Int32 version)
        {
            _aggregateVersion = version;
            return this;
        }

        public StatelessAggregateContextBuilder SetDataFactory(IDataFactory dataFactory)
        {
            _dataFactory = dataFactory;
            return this;
        }

        public IStatelessAggregateContext Build()
        {
            if (_aggregateId == null)
                _aggregateId = Guid.NewGuid().ToString();

            return new StatelessAggregateContext(_aggregateId, _aggregateVersion, _dataFactory);
        }
    }
}