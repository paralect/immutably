using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Comment_DeletedEvent : IMessage
    {
        public Guid CommentId { get; set; }
    }
}