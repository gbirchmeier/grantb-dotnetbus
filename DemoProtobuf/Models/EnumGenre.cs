using ProtoBuf;

namespace DemoProtobuf.Models;

[ProtoContract(Name = "Genre")]
public enum EnumGenre {
   Unset = 0,
   Rock = 1,
   HipHop = 2
}
