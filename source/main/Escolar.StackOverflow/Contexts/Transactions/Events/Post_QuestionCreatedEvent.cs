using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Events
{
    public class Post_QuestionCreatedEvent : IEvent
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }

        public String Tags { get; set; }
    }
}