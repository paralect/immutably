using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_CreateQuestionCommand
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }

        public String Tags { get; set; }
    }
}