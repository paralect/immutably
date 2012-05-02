using System;
using Escolar.StackOverflow.Enums;

namespace Escolar.StackOverflow.Domain
{
    public class Post
    {
        /// <summary>
        /// Serialization friendly, inner class for Post state
        /// </summary>
        public class PostState
        {
            /// <summary>
            /// Unique post ID
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Author ID
            /// </summary>
            public Guid UserId { get; set; }

            /// <summary>
            /// Site ID
            /// </summary>
            public Guid SiteId { get; set; }

            /// <summary>
            /// Either PostType.Question or PostType.Answer
            /// </summary>
            public PostType PostType { get; set; }

            /// <summary>
            /// Guid.Empty if PostType is Question.
            /// Question ID otherwise.
            /// </summary>
            public Guid QuestionId { get; set; }
            
            /// <summary>
            /// Title of post
            /// </summary>
            public String Title { get; set; }

            /// <summary>
            /// Content of post (in Markdown)
            /// </summary>
            public String Content { get; set; }
            
            /// <summary>
            /// Comma separated list of tags
            /// </summary>
            public String Tags { get; set; }

            /// <summary>
            /// Score of the post
            /// </summary>
            public Int32 Score { get; set; }
        }
    }
}