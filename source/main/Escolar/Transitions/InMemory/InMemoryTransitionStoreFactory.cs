namespace Escolar.Transitions
{
    public class InMemoryTransitionStoreFactory : ITransitionStoreFactory
    {
        public ITransitionStore CreateTransitionStore()
        {
            return new InMemoryTransitionStore();
        }
    }
}