using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Events
{
    public class User_UserCreatedEvent
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}