using ProtoBuf;

namespace DemoProtobuf.Models;

[ProtoContract]
public class Band {
    [ProtoMember(1)]
    public string Name { get; set; } = "Unset";

    [ProtoMember(2)]
    public EnumGenre Genre { get; set; }

    [ProtoMember(3)]
    public List<Musician> Members { get; set; } = new();

}
