using System;

namespace Lokad.Cqrs.TapeStorage
{
    public interface ITransitionStore
    {
        ITapeContainer GetContainer(String name);
    }
}