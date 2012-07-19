using System;
using System.Collections.Generic;
using System.IO;
using Immutably.Data;
using Immutably.Serialization.Abstract;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Immutably.Serialization
{
    public class ProtobufSerializer : ISerializer 
    {
        private readonly RuntimeTypeModel _model = TypeModel.Create();
        private readonly IDataContext _dataContext;

        public ProtobufSerializer(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public byte[] Serialize(IEnumerable<object> items)
        {
            using (var memoryStream = new MemoryStream())
            using (var binaryWriter = new BinaryWriter(memoryStream))
            {
                foreach (var item in items)
                {
                    var tag = _dataContext.GetTag(item.GetType());
                    binaryWriter.Write(tag.ToByteArray());
                    _model.SerializeWithLengthPrefix(memoryStream, item, item.GetType(), PrefixStyle.Base128, 0);
                }

                return memoryStream.ToArray();
            }
        }

        public IEnumerable<object> Deserialize(byte[] data)
        {
            List<Object> result = new List<object>();

            using (var memoryStream = new MemoryStream(data))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                while (true)
                {
                    try
                    {
                        var tag = new Guid(binaryReader.ReadBytes(4));
                        var obj = _model.DeserializeWithLengthPrefix(memoryStream, null, _dataContext.GetProxy(tag), PrefixStyle.Base128, 0);

                        if (obj == null)
                            break;

                        result.Add(obj);
                    }
                    catch (EndOfStreamException)
                    {
                        // planned exception, better performance than checking for the end of stream
                        break;
                    }

                }
            }

            return result;
        }
    }
}