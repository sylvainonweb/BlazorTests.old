using System.Net.Http;
using BlazorTests.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorTests.Client
{
    public abstract class ComponentBase : BlazorComponent
    {
        public string Title { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        protected int? GetNullableInt(int value)
        {
            return (value == (int)Constants.Null) ? new int?() : value;
        }
    }
}