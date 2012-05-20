using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class User_AboutChangedEvent : IEvent
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}