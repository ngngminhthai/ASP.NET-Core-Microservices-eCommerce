using Inventory.Grpc;

namespace Basket.API.Grpc
{
    public class GeeterGrpcService
    {
        private readonly Greeter.GreeterClient greeterClient;

        public GeeterGrpcService(Greeter.GreeterClient greeterClient)
        {
            this.greeterClient = greeterClient;
        }

        public async Task<string> GetGreetingAsync(string name)
        {
            var request = new HelloRequest { Name = name };

            var response = await greeterClient.SayHelloAsync(request);

            return response.Message;
        }
    }
}
