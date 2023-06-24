using Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Inventory;

namespace Orchestrator.HttpRepository;

public class InventoryHttpRepository : IInventoryHttpRepository
{
    private readonly HttpClient _client;

    public InventoryHttpRepository(HttpClient client)
    {
        _client = client;
    }

    public Task<string> CreateSalesOrder(PurchasedProduct model)
    {
        throw new NotImplementedException();
    }
}