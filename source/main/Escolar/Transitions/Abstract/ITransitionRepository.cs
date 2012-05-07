using System;
using System.Collections.Generic;

namespace Escolar.Transitions
{
    public interface ITransitionRepository
    {
        IList<ITransition> LoadTransitions(Guid streamId, Int32 fromVersion, Int32 toVersion);
        IList<ITransition> LoadAllTransitions();

        void Append(ITransition transition);
        
    }
}