using Orchestrator.HttpRepository.Interfaces;
using Shared.DTOs.Order;

namespace Orchestrator.HttpRepository;

public class OrderHttpRepository : IOrderHttpRepository
{
    private readonly HttpClient _client;

    public OrderHttpRepository(HttpClient client)
    {
        _client = client;
    }

    public Task<long> CreateOrder(OrderDto order)
    {
        throw new NotImplementedException();
    }
}