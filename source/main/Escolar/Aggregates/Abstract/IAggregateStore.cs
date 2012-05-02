namespace Escolar
{
    public interface IAggregateStore
    {
        IAggregateSession OpenSession();
    }
}