using ProtoBuf;

namespace DemoProtobuf.Models;

[ProtoContract]
public class Musician {
    [ProtoMember(1)]
    public string Name { get; set; } = "Unset";
}
