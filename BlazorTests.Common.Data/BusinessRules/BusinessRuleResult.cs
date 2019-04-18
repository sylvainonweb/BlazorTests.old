using System.Collections.Generic;

namespace BlazorTests.CommonData.Data.BusinessRules
{
    public class BusinessRuleResult
    {
        public string Message { get; set; }

        public BusinessRuleResult(string message)
        {
            this.Message = message;
        }
    }
}
