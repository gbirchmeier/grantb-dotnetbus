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

[ProtoContract]
public enum SideEnum {
    Unset = 0,
    Buy = 1,
    Sell = 2
}
