using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Post_ChangeContentCommand : ICommand
    {
        public Guid PostId { get; set; }
        public String Title { get; set; }
    }
}