using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;
using GrpcServer.Protos;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest { Name = "Kabbo"};

            //var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //var client = new Greeter.GreeterClient(channel);

            //var reply = await client.SayHelloAsync(input);
            //Console.WriteLine(reply.Message);

            var input = new CustomerLookupModel { UserId = 1 };

            var channel = GrpcChannel.ForAddress("http://localhost:5000");
            var client = new Customer.CustomerClient(channel);

            var reply = await client.GetCustomerInfoAsync(input);
            Console.WriteLine($"Hello {reply.FirstName} {reply.LastName} ...");
            Console.WriteLine();

            Console.WriteLine($"Customer List:");
            using (var call = client.GetCustomersInfo(new CustomersLookupModel()))
            {
                while (await call.ResponseStream.MoveNext())
                {
                    var customer = call.ResponseStream.Current;
                    Console.WriteLine($"{customer.FirstName} {customer.LastName} ...");
                }

            }
            Console.ReadLine();
        }
    }
}
