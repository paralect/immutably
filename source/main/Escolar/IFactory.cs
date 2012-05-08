using System;
using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar
{
    public interface IEscolarFactory
    {
        TType Create<TType>();

        TType Create<TType>(Action<TType> builder);
    }

    public class EscolarFactory : IEscolarFactory
    {
        private Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public EscolarFactory()
        {
            Register<IEventMetadata, EventMetadata>();
            Register<IEventEnvelope, EventEnvelope>();
            Register<IStateMetadata, StateMetadata>();
            Register<IStateEnvelope, StateEnvelope>();
        }

        private void Register<TFrom, TTo>()
        {
            _map[typeof (TFrom)] = typeof (TTo);
        }

        public TType Create<TType>()
        {
            throw new NotImplementedException();
        }

        public TType Create<TType>(Action<TType> builder)
        {
            throw new NotImplementedException();
        }


    }
}