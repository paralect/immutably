using System;
using Immutably.Messages;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Site_CreateCommand : ICommand 
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}