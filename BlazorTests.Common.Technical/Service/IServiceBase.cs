using BlazorTests.Common.Technical.Database;

namespace BlazorTests.Common.Technical.Service
{
    public interface IServiceBase
    {
        IRepository Repository { get; set; }
        string UserId { get; set; }
    }
}
