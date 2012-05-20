using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class User_ChangeAboutCommand : ICommand
    {
        public Guid UserId { get; set; }
        public String About { get; set; }
    }
}