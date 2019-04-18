

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmUserPasswordNotDefinedException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmUserPasswordNotDefinedException(string error)
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
