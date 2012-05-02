using System;

namespace Escolar.StackOverflow.Domain
{
    public class Comment
    {
        public class CommentState
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public Guid PostId { get; set; }
            public String Content { get; set; }
        }
    }
}