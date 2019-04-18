

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmIncorrectUserPasswordException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmIncorrectUserPasswordException(string error)
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
