using Dapper;
using EntityLib.Entities;
using Microsoft.Data.SqlClient;

namespace DbTool;

public static class DbOperation {

    public static void Create() {
        using (var cxn = new SqlConnection(Config.CxnString)) {
            cxn.Execute(TradeRequest.TableCreationQuery);
            cxn.Execute(TradeStatusUpdate.TableCreationQuery);
        }
    }

    public static void DropTables() {
        using (var cxn = new SqlConnection(Config.CxnString)) {
            cxn.Execute("DROP TABLE IF EXISTS TradeRequest");
            cxn.Execute("DROP TABLE IF EXISTS TradeStatusUpdate");
        }
    }
}
