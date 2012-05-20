using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class User_DeleteCommand : ICommand
    {
        public Guid UserId { get; set; }
    }
}