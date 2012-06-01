using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Site_CreateCommand
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}