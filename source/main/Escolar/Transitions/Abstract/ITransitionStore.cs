using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
{
    public interface ITransitionStore
    {
        ITransitionSession OpenSession();
    }

    public interface ITransitionSession : IDisposable
    {
        IList<ITransition> LoadTransitions(Guid streamId);

        void Append(IList<IEventEnvelope> transitions);
        void Append(params IEventEnvelope[] eventEnvelope);

        void Append(IList<ITransition> transitions);
        void Append(params ITransition[] transitions);

        void SaveChanges();
    }
}