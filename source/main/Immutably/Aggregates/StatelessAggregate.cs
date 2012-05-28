using System;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Stateless aggregate
    /// </summary>
    public abstract class StatelessAggregate : AggregateBase, IStatelessAggregate
    {

        public new IStatelessAggregateContext Context
        {
            get
            {
                return (IStatelessAggregateContext) base.Context;
            }
        }

        protected override IAggregateContext CreateAggregateContext()
        {
            return new StatelessAggregateContext(Guid.NewGuid().ToString(), 0, null);
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