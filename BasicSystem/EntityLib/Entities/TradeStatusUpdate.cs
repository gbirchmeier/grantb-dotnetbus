using ProtoBuf;

namespace EntityLib.Entities;

[ProtoContract]
public class TradeStatusUpdate {

    public TradeStatusUpdate() { } // deserialization needs a no-param ctor

    public TradeStatusUpdate(
        string tradeSourceId,
        TradeStatusEnum tradeStatus
    ) {
        CreationTime = DateTime.Now;
        TradeSourceId = tradeSourceId;
        TradeStatus = tradeStatus;
    }

    [ProtoMember(1)]
    public DateTime CreationTime { get; }

    [ProtoMember(2)]
    public string TradeSourceId { get; }

    [ProtoMember(3)]
    public TradeStatusEnum TradeStatus { get; }

    public const string TableCreationQuery =
        """
            CREATE TABLE TradeStatusUpdate(
                Id int Identity(1,1) NOT NULL,
                CreationTime datetime NOT NULL,
                TradeSourceId varchar(40) NOT NULL,
                TradeStatus tinyint NOT NULL,
                CONSTRAINT PK_TradeStatusUpdate PRIMARY KEY CLUSTERED
                (
                    [Id] ASC
                )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                       ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
                       ) ON [PRIMARY]
            ) ON [PRIMARY];
        """;

    public const string InsertTemplate =
        """
        INSERT INTO TradeStatusUpdate
        (CreationTime,TradeSourceId,TradeStatus)
        VALUES
        (@CreationTime,@TradeSourceId,@TradeStatus)
        """;
}
