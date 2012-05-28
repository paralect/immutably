using System;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Stateless aggregate
    /// </summary>
    public abstract class StatelessAggregate : AggregateBase, IStatelessAggregate
    {
        public override IAggregateContext Context
        {
            get
            {
                if (_context == null)
                    _context = new StatelessAggregateContext("temporary_id", 0, null);

                return _context;
            }
        }

        public void EstablishContext(IStatelessAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

        public void EstablishContext(Action<StatelessAggregateContextBuilder> contextBuilder)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            var builder = new StatelessAggregateContextBuilder();
            contextBuilder(builder);
            _context = builder.Build();
        }

    }
}