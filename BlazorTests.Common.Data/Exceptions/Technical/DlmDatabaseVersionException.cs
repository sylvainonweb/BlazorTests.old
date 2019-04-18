

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmDatabaseVersionException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmDatabaseVersionException(string error)
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
