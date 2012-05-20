using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Comment_DeletedEvent : IEvent
    {
        public Guid CommentId { get; set; }
    }
}