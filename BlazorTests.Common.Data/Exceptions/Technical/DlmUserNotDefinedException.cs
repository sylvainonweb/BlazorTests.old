

namespace BlazorTests.Common.Data.Exceptions
{
    public class DlmUserNotDefinedException : DlmTechnicalException
    {
        private string Error { get; set; }
        public DlmUserNotDefinedException(string error)
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
