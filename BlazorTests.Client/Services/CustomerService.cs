using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorTests.Client.Services
{
    public class CustomerService : HttpServiceBase
    {
        public CustomerService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public Task<IList<Customer>> GetCustomers(int? customerTypeId)
        {
            if (customerTypeId.HasValue)
            {
                return HttpClient.GetJsonAsync<IList<Customer>>($"api/Customer?customerTypeId={GetIntAsString(customerTypeId)}");
            }
            else
            {
                return HttpClient.GetJsonAsync<IList<Customer>>($"api/Customer");
            }            
        }

        public Task<Customer> GetCustomer(int customerId)
        {
            return HttpClient.GetJsonAsync<Customer>($"api/Customer/{customerId}");
        }

        public Task<HttpResponseMessage> DeleteCustomer(int customerId)
        {
            return HttpClient.DeleteAsync($"api/Customer/{customerId}");
        }

        public Task SaveCustomer(Customer customer)
        {
            return HttpClient.PostJsonAsync("api/Customer/Save", customer);
        }
    }
}
