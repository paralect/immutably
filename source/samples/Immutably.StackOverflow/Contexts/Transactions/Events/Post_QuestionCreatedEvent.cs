using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_QuestionCreatedEvent
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }

        public String Tags { get; set; }
    }
}