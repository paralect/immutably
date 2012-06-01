using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class Post_DeletedEvent : IMessage
    {
        public Guid PostId { get; set; }
    }
}