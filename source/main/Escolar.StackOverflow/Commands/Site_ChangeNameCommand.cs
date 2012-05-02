using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Site_ChangeNameCommand : ICommand
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
    }
}