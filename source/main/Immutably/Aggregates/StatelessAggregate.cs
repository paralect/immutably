using System;

namespace Immutably.Aggregates
{
    /// <summary>
    /// Aggregate without state
    /// </summary>
    public abstract class StatelessAggregate : AggregateBase, IStatelessAggregate
    {
        /// <summary>
        /// Stateless aggregate context
        /// </summary>
        public new IStatelessAggregateContext Context
        {
            get { return (IStatelessAggregateContext) base.Context; }
        }

        /// <summary>
        /// Template method override to create stateless aggregate context
        /// </summary>
        protected override IAggregateContext CreateAggregateContext()
        {
            return new StatelessAggregateContext(Guid.NewGuid().ToString(), 0, null);
        }

        /// <summary>
        /// Establish context for this stateless aggregate
        /// You can establish context only one time. Subsequent calls to this method will 
        /// lead to AggregateContextModificationForbiddenException exception.
        /// </summary>
        public void EstablishContext(IStatelessAggregateContext context)
        {
            if (_context != null)
                throw new AggregateContextModificationForbiddenException(GetType());

            _context = context;
        }

        /// <summary>
        /// Establish context for this stateless aggregate via context builder
        /// You can establish context only one time. Subsequent calls to this method will 
        /// lead to AggregateContextModificationForbiddenException exception.
        /// </summary>
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