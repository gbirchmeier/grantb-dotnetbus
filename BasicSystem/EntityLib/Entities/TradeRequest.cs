using ProtoBuf;

namespace EntityLib.Entities;

[ProtoContract]
public class TradeRequest {

    public TradeRequest() { } // deserialization needs a no-param ctor

    public TradeRequest(
        string traderId,
        string sourceId,
        OrderTypeEnum orderType,
        string symbol,
        SideEnum side
    ) {
        CreationTime = DateTime.Now;
        TraderId = traderId;
        SourceId = sourceId;
        OrderType = orderType;
        Symbol = symbol;
        Side = side;
    }

    [ProtoMember(1)]
    public DateTime CreationTime { get; }

    [ProtoMember(2)]
    public string TraderId { get; }

    [ProtoMember(3)]
    public string SourceId { get; }

    [ProtoMember(4)]
    public OrderTypeEnum OrderType { get; }

    [ProtoMember(5)]
    public string Symbol { get; }

    [ProtoMember(6)]
    public decimal? Price { get; set; } = null;

    [ProtoMember(7)]
    public SideEnum Side { get; }
}
