using System;

namespace Immutably.StackOverflow.Contexts.Transactions.Commands
{
    public class Site_ChangeDescriptionCommand
    {
        public Guid SiteId { get; set; }
        public String Description { get; set; }
    }
}