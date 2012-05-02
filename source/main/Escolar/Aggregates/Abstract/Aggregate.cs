namespace Escolar.Aggregates.Abstract
{
    public class Aggregate<TState>
    {
        /// <summary>
        /// User state
        /// </summary>
        protected TState _state;
    }
}