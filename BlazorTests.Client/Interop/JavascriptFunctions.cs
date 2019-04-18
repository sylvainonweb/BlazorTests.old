

using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorTests.Client.Controls
{
    public class JavascriptFunctions
    {
        public static Task<string> ShowAlert(string message)
        {
            return JSRuntime.Current.InvokeAsync<string>("JavascriptFunctions.showAlert", message);
        }

        public static Task<string> InitTable(string tableId)
        {
            return JSRuntime.Current.InvokeAsync<string>("JavascriptFunctions.initTable", tableId);
        }
    }
}