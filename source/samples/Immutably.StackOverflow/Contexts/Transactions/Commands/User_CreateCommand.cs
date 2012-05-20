using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class User_CreateCommand : ICommand
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}