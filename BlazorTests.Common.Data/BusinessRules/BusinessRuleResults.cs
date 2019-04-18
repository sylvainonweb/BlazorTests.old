using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlazorTests.CommonData.Data.BusinessRules
{
    public class BusinessRuleResults<T> : List<T> where T : BusinessRuleResult
    {
        public BusinessRuleErrors GetErrors()
        {
            BusinessRuleErrors errors = new BusinessRuleErrors();
            foreach (BusinessRuleResult ruleResult in this)
            {
                if (ruleResult is BusinessRuleError)
                {
                    errors.Add(ruleResult as BusinessRuleError);
                }
            }

            return errors;
        }

        public BusinessRuleWarnings GetWarnings()
        {
            BusinessRuleWarnings warnings = new BusinessRuleWarnings();
            foreach (BusinessRuleResult ruleResult in this)
            {
                if (ruleResult is BusinessRuleWarning)
                {
                    warnings.Add(ruleResult as BusinessRuleWarning);
                }
            }

            return warnings;
        }

        public string ConvertToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (BusinessRuleResult ruleResult in this)
            {
                builder.AppendLine(ruleResult.Message);
            }

            return builder.ToString();
        }
    }
}
