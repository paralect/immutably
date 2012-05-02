using System;
using Paralect.Machine.Processes;

namespace Escolar.States
{
    public interface IStateHelper
    {
    }

    public class StateHelper : IStateHelper
    {
        private readonly IFactory _factory;

        public StateHelper(IFactory factory)
        {
            _factory = factory;
        }


    }
}