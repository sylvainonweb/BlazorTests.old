using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTests.CommonData.Data.BusinessRules
{
    public class BusinessRuleWarning : BusinessRuleResult
    {
        public BusinessRuleWarning(string message) : base(message)
        {
        }
    }
}
