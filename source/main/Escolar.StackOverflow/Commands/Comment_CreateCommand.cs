using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Comment_CreateCommand : ICommand
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public String Content { get; set; }
    }
}