using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Post_CreateAnswerCommand : ICommand
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }
        public Guid QuestionId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }
    }
}