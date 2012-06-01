using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Site_ChangeNameCommand
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
    }
}