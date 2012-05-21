using System;

namespace Immutably.Aggregates
{
    public class InvalidAggregateIdException : Exception
    {
        public InvalidAggregateIdException() : base(String.Format(
            "Aggregate ID cannot be null for reference types"))
        {
        }
    }

    public class InvalidAggregateStateException : Exception
    {
        public InvalidAggregateStateException(Type stateType) : base(String.Format(
            "Aggregate State cannot be default({0})", stateType.Name))
        {
        }        
    }

    public class InvalidAggregateVersionException : Exception
    {
        public InvalidAggregateVersionException(Int32 version) : base(String.Format(
            "Aggregate Version cannot be less than zero. It is now ({0})", version))
        {
        }        
    }

    public class AggregateContextModificationForbiddenException : Exception
    {
        public AggregateContextModificationForbiddenException(Type aggregateType) : base(String.Format(
            "Aggregate Context can be setuped one time only. " +
            "Subsequent modifications to Contexts will lead to this exception. " +
            "Attempt was to modify Context of [{0}] aggregate.", aggregateType.FullName))
        {
        }        
    }
}