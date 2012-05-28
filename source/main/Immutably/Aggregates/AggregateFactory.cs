using System;
using System.Collections.Generic;
using System.Reflection;

namespace Immutably.Aggregates
{
    public enum AggregateKind
    {
        Statefull = 0,
        Stateless = 1
    }

    public class AggregateDefinition
    {
        /// <summary>
        /// Type of aggregate
        /// </summary>
        public Type AggregateType { get; set; }

        /// <summary>
        /// Null if this is stateless aggregate
        /// </summary>
        public Type StateType { get; set; }

        /// <summary>
        /// Gets if this aggregate is statefull
        /// </summary>
        public AggregateKind AggregateKind { get; set; }
    }

    public class AggregateFactory
    {
        private Dictionary<Type, AggregateDefinition> _definitions = new Dictionary<Type, AggregateDefinition>();

        public AggregateDefinition GetAggregateDefinition(Type aggregateType)
        {
            AggregateDefinition definition;
            var contains = _definitions.TryGetValue(aggregateType, out definition);

            if (!contains)
                _definitions[aggregateType] = definition = Register(aggregateType);

            return definition;
        }

        public AggregateDefinition Register(Type aggregateType)
        {
            AggregateDefinition definition = new AggregateDefinition();
            definition.AggregateType = aggregateType;

            var statefull = aggregateType.GetInterface(typeof(IStatefullAggregate).FullName);
            if (statefull != null)
            {
                definition.AggregateKind = AggregateKind.Statefull;

                if (aggregateType.BaseType == null
                    || aggregateType.BaseType.IsGenericType == false
                    || aggregateType.BaseType.GetGenericTypeDefinition() != typeof(StatefullAggregate<>))
                    throw new Exception(String.Format("We cannot find state type for [{0}] aggregate", aggregateType.FullName));

                var genericArgs = aggregateType.BaseType.GetGenericArguments();
                definition.StateType = genericArgs[0];
                return definition;
            }

            var stateless = aggregateType.GetInterface(typeof(IStatelessAggregate).FullName);
            if (stateless != null)
            {
                definition.AggregateKind = AggregateKind.Stateless;
                return definition;
            }

            throw new Exception(String.Format("Object of type ({0}) not an aggregate", aggregateType));
        }
    }
}