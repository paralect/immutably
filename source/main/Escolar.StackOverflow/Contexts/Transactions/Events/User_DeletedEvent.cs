using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class User_DeletedEvent : IEvent
    {
        public Guid UserId { get; set; }
    }
}