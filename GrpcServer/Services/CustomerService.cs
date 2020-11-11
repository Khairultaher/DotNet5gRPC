using Grpc.Core;
using GrpcServer.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Afifah";
                output.LastName = "Tasneem";
            }
            if (request.UserId == 2)
            {
                output.FirstName = "Omar";
                output.LastName = "Abdullah";
            }
            return Task.FromResult(output);
        }
        public override async Task GetCustomersInfo(CustomersLookupModel request
            , IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                     FirstName = "Afifah",
                     LastName = "Tasneem"
                },
                new CustomerModel
                {
                    FirstName = "Omar",
                    LastName = "Abdullah"
                }
            };
            foreach (var customer in customers)
            {
                // await Task.Delay(1000); 
                await responseStream.WriteAsync(customer);
            }          
        }
    }
}
