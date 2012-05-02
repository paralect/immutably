using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.EventStore
{
    public interface IEventStore
    {
        /// <summary>
        /// 
        /// </summary>
        IList<IEventEnvelope> GetById(Guid id);

        /// <summary>
        /// 
        /// </summary>
        void Append(IList<IEventEnvelope> events);
    }
}