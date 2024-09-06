using ProtoBuf;

namespace EntityLib.Entities;

[ProtoContract]
public class TradeStatusUpdate {

    public TradeStatusUpdate() { } // deserialization needs a no-param ctor

    public TradeStatusUpdate(
        string tradeSourceId,
        TradeStatusEnum tradeStatusEnum
    ) {
        CreationTime = DateTime.Now;
        TradeSourceId = tradeSourceId;
        TradeStatusEnum = tradeStatusEnum;
    }

    [ProtoMember(1)]
    public DateTime CreationTime { get; }

    [ProtoMember(2)]
    public string TradeSourceId { get; }

    [ProtoMember(3)]
    public TradeStatusEnum TradeStatusEnum { get; }
}
