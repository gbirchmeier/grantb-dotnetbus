using ProtoBuf;

namespace EntityLib.Entities;

[ProtoContract]
public class TradeRequest {
    public TradeRequest(
        string traderId,
        string sourceId,
        OrderTypeEnum orderTypeEnum,
        string symbol
    ) {
        CreationTime = DateTime.Now;
        TraderId = traderId;
        SourceId = sourceId;
        OrderTypeEnum = orderTypeEnum;
        Symbol = symbol;
    }

    [ProtoMember(1)]
    public DateTime CreationTime { get; }

    [ProtoMember(2)]
    public string TraderId { get; }

    [ProtoMember(3)]
    public string SourceId { get; }

    [ProtoMember(4)]
    public OrderTypeEnum OrderTypeEnum { get; }

    [ProtoMember(5)]
    public string Symbol { get; }

    [ProtoMember(6)]
    public decimal? Price { get; set; } = null;
}
