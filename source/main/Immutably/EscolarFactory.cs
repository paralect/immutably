using System;
using System.Collections.Generic;
using Escolar.Messages;
using Paralect.Machine.Processes;

namespace Escolar
{
    public class EscolarFactory : IEscolarFactory
    {
        private Dictionary<Type, Type> _map = new Dictionary<Type, Type>();

        public EscolarFactory()
        {
//            Register<IEventMetadata, EventMetadata>();
  //          Register<IEventEnvelope, EventEnvelope>();
    //        Register<IStateMetadata, StateMetadata>();
      //      Register<IStateEnvelope, StateEnvelope>();
        }

        private void Register<TFrom, TTo>()
        {
            _map[typeof (TFrom)] = typeof (TTo);
        }

        public TType Create<TType>()
        {
            return Activator.CreateInstance<TType>();
        }

        public TType Create<TType>(Action<TType> builder)
        {
            var obj = Activator.CreateInstance<TType>();
            builder(obj);
            return obj;
        }
    }
}