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

    public const string TableCreationQuery =
        """
            CREATE TABLE TradeRequest(
                Id int Identity(1,1) NOT NULL,
                CreationTime datetime NOT NULL,
                TraderId varchar(40) NOT NULL,
                SourceId varchar(40) NOT NULL,
                OrderType tinyint NOT NULL,
                Symbol varchar(20) NOT NULL,
                Price decimal NULL,
                Side tinyint NOT NULL,
                CONSTRAINT PK_TradeRequest PRIMARY KEY CLUSTERED
                (
                    [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                       ) ON [PRIMARY]
            ) ON [PRIMARY];
        """;

    public const string InsertTemplate =
        """
        INSERT INTO TradeRequest
        (CreationTime,TraderId,SourceId,OrderType,Symbol,Price,Side)
        VALUES
        (@CreationTime,@TraderId,@SourceId,@OrderType,@Symbol,@Price,@Side)
        """;
}
