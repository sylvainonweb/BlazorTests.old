using System.Collections.Generic;

namespace BlazorTests.CommonData.Data.BusinessRules
{
    public class BusinessRuleError : BusinessRuleResult
    {
        public BusinessRuleError(string message) : base(message)
        {
        }
    }
}
