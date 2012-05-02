using System;
using System.Collections.Generic;
using Escolar.Messages;

namespace Escolar.Transitions
{
    public interface ITransitionStore
    {
        /// <summary>
        /// 
        /// </summary>
        IList<ITransition> GetById(Guid id);

        /// <summary>
        /// 
        /// </summary>
        void Append(IList<ITransition> transitions);
    }
}