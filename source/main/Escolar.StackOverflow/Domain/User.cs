using System;

namespace Escolar.StackOverflow.Domain
{
    public class User
    {
        /// <summary>
        /// Serialization friendly, inner class for User state
        /// </summary>
        public class UserState
        {
            public Guid Id { get; set; }
            public Guid About { get; set; }

            public Int32 Reputation { get; set; }
        }

        /// <summary>
        /// User state
        /// </summary>
        private readonly UserState _state;

        /// <summary>
        /// Create user state
        /// </summary>
        public User(UserState state)
        {
            _state = state ?? new UserState();
        }
    }
}