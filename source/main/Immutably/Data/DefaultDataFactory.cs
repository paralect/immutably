using System;

namespace Immutably.Data
{
    /// <summary>
    /// Default data factory.
    /// Creates objects by calling System.Activator methods
    /// </summary>
    public class DefaultDataFactory : IDataFactory
    {
        public TDataType Create<TDataType>()
        {
            return (TDataType)Create(typeof(TDataType));
        }

        public object Create(Type type)
        {
            return Activator.CreateInstance(type);
        }

        public TDataType Create<TDataType>(Action<TDataType> builder)
        {
            var obj = Create<TDataType>();
            builder(obj);
            return obj;
        }         
    }
}