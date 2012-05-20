using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Site_DeleteCommand : ICommand
    {
        public Guid SiteId { get; set; }
    }
}