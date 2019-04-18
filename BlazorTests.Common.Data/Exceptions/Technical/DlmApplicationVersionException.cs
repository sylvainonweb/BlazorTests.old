

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmApplicationVersionException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmApplicationVersionException(string error)
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
