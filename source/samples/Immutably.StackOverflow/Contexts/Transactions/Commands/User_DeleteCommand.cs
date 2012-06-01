using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class User_DeleteCommand
    {
        public Guid UserId { get; set; }
    }
}