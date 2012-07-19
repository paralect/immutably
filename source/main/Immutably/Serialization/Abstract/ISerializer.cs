using System;
using System.Collections.Generic;
using Immutably.Data;

namespace Immutably.Serialization.Abstract
{
    public interface ISerializer
    {
        byte[] Serialize(IEnumerable<Object> items);
        IEnumerable<Object> Deserialize(byte[] data);
    }
}