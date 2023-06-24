namespace Inventory.Grpc.Services;
using global::Grpc.Core;
using Protos;

public class InventoryService : StockProtoService.StockProtoServiceBase
{
    public override async Task<StockModel> GetStock(GetStockRequest request, ServerCallContext context)
    {
        var result = new StockModel()
        {
            Quantity = 1
        };
        return result;
    }

}