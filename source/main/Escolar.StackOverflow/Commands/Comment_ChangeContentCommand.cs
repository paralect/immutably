using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Comment_ChangeContentCommand : ICommand
    {
        public Guid CommentId { get; set; }
        public String Content { get; set; }
    }
}