using System;

namespace Escolar.Data
{
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

    [DataContract("E9B3D797-7A8B-4AA0-AD12-6867473DC717")]
    public class ProtobufMessage
    {
        
    }

    [DataContract("HFGD734-56FB-86A0-GF12-5787473D46AB")]
    public class MyMessage
    {
        
    }

    [DataContract("{71CCD462-7DF9-418A-AC0D-F938E2251282}")]
    public class MyBiggerMessage : MyMessage
    {
        
    }

    [DataProxy]
    public class MyMessage_Protobuf : MyMessage
    {
        
    }

    [DataProxy]
    public class MyBiggerMessage_Protobuf : MyBiggerMessage
    {
        
    }
}