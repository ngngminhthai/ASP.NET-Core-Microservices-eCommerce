namespace Basket.API.GrpcServices;

using Inventory.Grpc.Protos;

public class StockItemGrpcService
{
    private readonly StockProtoService.StockProtoServiceClient _stockProtoService;

    public StockItemGrpcService(StockProtoService.StockProtoServiceClient stockProtoService)
    {
        _stockProtoService = stockProtoService ?? throw new ArgumentNullException(nameof(stockProtoService));
    }

    public async Task<StockModel> GetStock()
    {
        try
        {
            var stockItemRequest = new GetStockRequest { ProductId = 10 };
            var result = await _stockProtoService.GetStockAsync(stockItemRequest);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}