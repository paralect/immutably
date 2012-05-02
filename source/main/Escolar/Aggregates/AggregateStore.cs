namespace Escolar
{
    public class AggregateStore : IAggregateStore
    {
        public IAggregateSession OpenSession()
        {
            return new AggregateSession();
        }
    }
}