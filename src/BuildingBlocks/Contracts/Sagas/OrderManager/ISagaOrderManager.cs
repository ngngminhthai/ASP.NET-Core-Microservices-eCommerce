namespace Contracts.Sagas.OrderManager;

public interface ISagaOrderManager<in TInput, out TOutput> where TInput: class
where TOutput : class
{
    public TOutput CreateOrder(TInput input);
    public TOutput RollbackOrder(string username, string documentNo, long orderId);
}