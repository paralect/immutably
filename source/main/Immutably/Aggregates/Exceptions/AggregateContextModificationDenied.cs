using System;

namespace Immutably.Aggregates
{
    public class AggregateContextModificationDeniedException : Exception
    {
        public AggregateContextModificationDeniedException(string message) : base(message)
        {
        }
    }
}