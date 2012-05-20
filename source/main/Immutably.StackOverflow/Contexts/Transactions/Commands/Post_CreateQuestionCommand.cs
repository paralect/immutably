using System;
using Escolar.Messages;

namespace Escolar.StackOverflow.Commands
{
    public class Post_CreateQuestionCommand : ICommand
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public Guid SiteId { get; set; }

        public String Title { get; set; }
        public String Content { get; set; }

        public String Tags { get; set; }
    }
}