using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlazorTests.Common.Data.Exceptions;

namespace BlazorTests.CommonData.Data.BusinessRules
{
    public class BusinessRules : List<IBusinessRule>
    {
        private BusinessRuleResults<BusinessRuleResult> Results = new BusinessRuleResults<BusinessRuleResult>();

        /// <summary>
        /// Renvoie une exception en cas d'erreur bloquante et retourne la liste des erreurs non bloquantes (warning)
        /// </summary>
        /// <returns></returns>
        public BusinessRuleWarnings CheckRules()
        {
            this.Results.Clear();

            foreach (IBusinessRule rule in this)
            {
                BusinessRuleResult ruleResult = rule.CheckRule();
                if (ruleResult != null)
                {
                    // Si le message n'a pas déja été rajouté
                    if (this.Results.Count(o => o.Message == ruleResult.Message) == 0)
                    {
                        this.Results.Add(ruleResult);
                    }
                }
            }

            if (Results.GetErrors().Count > 0)
            {
                throw new DlmBusinessException(Results.GetErrors().ConvertToString());
            }

            return this.Results.GetWarnings();
        }
    }
}