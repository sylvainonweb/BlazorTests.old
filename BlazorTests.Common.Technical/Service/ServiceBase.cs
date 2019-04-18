using BlazorTests.Common.Technical.Database;

namespace BlazorTests.Common.Technical.Service
{
    public class ServiceBase : IServiceBase
    {
        public IRepository Repository { get; set; }
        public string UserId { get; set; } = ""; // FIXME SBD : Voir comment on gère ça
    }
}
