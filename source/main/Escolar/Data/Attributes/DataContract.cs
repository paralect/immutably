using System;

namespace Escolar.Data
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DataContract : Attribute
    {
        private readonly string _tag;

        public string Tag
        {
            get { return _tag; }
        }

        public DataContract(String tag)
        {
            _tag = tag;
        }
    }
}