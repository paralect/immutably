using System;

namespace Immutably.Aggregates
{
    public class AggregateAttribute : Attribute
    {
        private readonly string _aggregateName;

        public string AggregateName
        {
            get { return _aggregateName; }
        }

        public AggregateAttribute(String aggregateName)
        {
            _aggregateName = aggregateName;
        }
    }
}