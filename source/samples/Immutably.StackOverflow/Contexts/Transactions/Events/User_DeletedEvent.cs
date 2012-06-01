using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class User_DeletedEvent
    {
        public Guid UserId { get; set; }
    }
}