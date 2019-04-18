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
    public class AdministrationService : HttpServiceBase
    {
        public AdministrationService(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public Task<IList<CustomerType>> GetCustomerTypes()
        {
            return HttpClient.GetJsonAsync<IList<CustomerType>>("api/Administration/CustomerType");
        }

        public Task<CustomerType> GetCustomerType(int customerTypeId)
        {
            return HttpClient.GetJsonAsync<CustomerType>($"api/Administration/CustomerType/{customerTypeId}");
        }

        public Task<HttpResponseMessage> DeleteCustomerType(int customerTypeId)
        {
            return HttpClient.DeleteAsync($"api/Administration/CustomerType/{customerTypeId}");
        }

        public Task SaveCustomerType(CustomerType customerTypeId)
        {
            return HttpClient.PostJsonAsync("api/Administration/CustomerType/Save", customerTypeId);
        }
    }
}
