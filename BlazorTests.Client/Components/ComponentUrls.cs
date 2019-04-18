using System.Net.Http;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorTests.Client
{
    public class ComponentUrls
    {
        public string CustomerList = "/Customer";
        public string CustomerEdit = "/Customer/Edit";

        public string CustomerTypeList = "/Administration/CustomerType";
        public string CustomerTypeEdit = "/Administration/CustomerType/Edit";
    }
}