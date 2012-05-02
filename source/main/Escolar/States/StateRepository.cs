using System;

namespace Escolar.States
{
    public interface StateRepository<TState>
    {
        TState GetById(Guid id);
        
    }
}