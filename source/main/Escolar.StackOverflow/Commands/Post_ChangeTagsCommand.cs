using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Post_ChangeTagsCommand : ICommand
    {
        public Guid PostId { get; set; }
        public String Tags { get; set; }
    }
}