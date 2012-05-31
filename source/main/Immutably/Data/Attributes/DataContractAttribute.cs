using System;

namespace Immutably.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataContractAttribute : Attribute
    {
        private readonly string _tag;

        public string Tag
        {
            get { return _tag; }
        }

        public DataContractAttribute(String tag)
        {
            _tag = tag;
        }
    }
}