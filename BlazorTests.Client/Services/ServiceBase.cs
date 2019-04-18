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
    public abstract class HttpServiceBase
    {
        protected HttpClient HttpClient { get; set; }

        public string GetDateAsString(DateTime? date)
        {
            if (date.HasValue)
            {
                return date.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                return string.Empty;
            }
        }

        public string GetDecimalAsString(decimal? value)
        {
            if (value.HasValue)
            {
                return value.ToString().Replace(",", ".");
            }

            return string.Empty;
        }

        public string GetIntAsString(int? value)
        {
            if (value.HasValue)
            {
                return value.ToString();
            }

            return string.Empty;
        }
    }
}
