using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Post_TagsChangedEvent : IEvent
    {
        public Guid SenderId { get; set; }
        public String Tags { get; set; }
    }
}