using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class User_UserCreatedEvent : IEvent
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}