using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class User_CreateCommand
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}