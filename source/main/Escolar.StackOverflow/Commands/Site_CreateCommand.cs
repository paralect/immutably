using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Site_CreateCommand : ICommand 
    {
        public Guid SiteId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
    }
}