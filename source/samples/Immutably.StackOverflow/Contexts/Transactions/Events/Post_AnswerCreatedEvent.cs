using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_AnswerCreatedEvent
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }
        public Guid QuestionId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }
    }
}