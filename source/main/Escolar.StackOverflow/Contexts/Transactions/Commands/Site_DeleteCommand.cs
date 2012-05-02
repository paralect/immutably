using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Site_DeleteCommand : ICommand
    {
        public Guid SiteId { get; set; }
    }
}