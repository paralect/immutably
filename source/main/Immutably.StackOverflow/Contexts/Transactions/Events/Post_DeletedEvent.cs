using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Post_DeletedEvent : IEvent
    {
        public Guid PostId { get; set; }
    }
}