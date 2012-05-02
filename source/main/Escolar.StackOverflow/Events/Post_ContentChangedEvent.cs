using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Post_ContentChangedEvent : IEvent
    {
        public Guid SenderId { get; set; }
        public String Title { get; set; }
    }
}