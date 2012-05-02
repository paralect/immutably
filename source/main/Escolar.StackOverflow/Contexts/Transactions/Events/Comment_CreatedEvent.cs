using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Comment_CreatedEvent : IEvent
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public String Content { get; set; }
    }
}