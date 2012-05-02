using System;

namespace Escolar.StackOverflow.Domain
{
    public class Site
    {
        /// <summary>
        /// Serialization friendly, inner class for Site state
        /// </summary>
        public class SiteState
        {
            public String Id { get; set; }
            public String Name { get; set; }
            public String Description { get; set; }
        }

        /// <summary>
        /// User state
        /// </summary>
        private readonly SiteState _state;

        /// <summary>
        /// Create user state
        /// </summary>
        public Site(SiteState state)
        {
            _state = state ?? new SiteState();
        }
    }
}