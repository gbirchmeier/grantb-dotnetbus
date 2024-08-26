using ProtoBuf;

namespace EntityLib.Entities;

[ProtoContract]
public enum OrderTypeEnum {
    Unset = 0,
    Market = 1
}

[ProtoContract]
public enum TradeStatusEnum {
    Unset = 0,
    Filled = 1,
    Rejected = 2
}
