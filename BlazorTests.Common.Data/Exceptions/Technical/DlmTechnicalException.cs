

namespace BlazorTests.Common.Data.Exceptions
{
    public abstract class DlmTechnicalException : DlmExceptionBase
    {
        public bool IsLoggable { get { return true; } }
    }
}
