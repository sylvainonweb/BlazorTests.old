using BlazorTests.Common.Technical.Database;
using Microsoft.AspNetCore.Mvc;

namespace BlazorTests.Server.Controllers
{
    public class ControllerEx : Controller
    {
        public IRepository Repository { get; set; }
    }
}
