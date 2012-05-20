using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Comment_ContentChangedEvent : IEvent
    {
        public Guid CommentId { get; set; }
        public String Content { get; set; }
    }
}