using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Site_ChangeDescriptionCommand : ICommand
    {
        public Guid SiteId { get; set; }
        public String Description { get; set; }
    }
}