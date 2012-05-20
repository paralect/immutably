using System;

namespace Escolar.Aggregates
{
    public class AggregateContextModificationDeniedException : Exception
    {
        public AggregateContextModificationDeniedException(string message) : base(message)
        {
        }
    }
}