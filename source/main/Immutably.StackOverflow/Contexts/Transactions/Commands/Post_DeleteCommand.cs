using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Post_DeleteCommand : ICommand
    {
        public Guid PostId { get; set; }
    }
}