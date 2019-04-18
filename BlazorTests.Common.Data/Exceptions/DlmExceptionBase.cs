
using System;

namespace BlazorTests.Common.Data.Exceptions
{
    public abstract class DlmExceptionBase : ApplicationException
    {
        bool IsLoggable { get; }
    }
}
