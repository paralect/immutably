using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Comment_DeleteCommand : ICommand
    {
        public Guid CommentId { get; set; }
    }
}