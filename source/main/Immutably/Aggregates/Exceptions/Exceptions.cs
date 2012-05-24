using System;

namespace Immutably.Aggregates
{
    public class NullAggregateIdException : Exception
    {
        public NullAggregateIdException() : base(String.Format(
            "Aggregate ID cannot be null for reference types"))
        {
        }
    }

    public class NullAggregateStateException : Exception
    {
        public NullAggregateStateException() : base(String.Format(
            "Aggregate State cannot be null"))
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

    public class AggregateDoesntExistException : Exception
    {
        public AggregateDoesntExistException(Type aggregateType, Object id) : base(String.Format(
            "Aggregate ({0}) with id ({1}) desn't exist in the store. ", aggregateType.FullName, id.ToString()))
        {
        }               
    }
}