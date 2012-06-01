using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class User_DeletedEvent : IMessage
    {
        public Guid UserId { get; set; }
    }
}