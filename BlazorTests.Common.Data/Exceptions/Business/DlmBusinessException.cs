
using System.Collections.Generic;

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmBusinessException : DlmExceptionBase
    {
        public bool IsLoggable
        {
            get { return false; }
        }

        private string Error { get; set; }
        public DlmBusinessException(string error)
        {
            this.Error = error;
        }

        public override string Message
        {
            get
            {
                return this.Error;
            }
        }
    }
}
