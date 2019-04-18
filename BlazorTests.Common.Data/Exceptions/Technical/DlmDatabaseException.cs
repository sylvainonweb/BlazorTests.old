

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmDatabaseException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmDatabaseException(string error)
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
