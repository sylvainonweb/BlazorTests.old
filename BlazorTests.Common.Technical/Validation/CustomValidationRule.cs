using System;
using System.Collections.Generic;

namespace BlazorTests.Common.Technical.Validation
{
    public class CustomValidationRule : ValidationRule
    {
        private Func<bool> Function { get; set; }
        public string ErrorMessage { get; protected set; }

        public CustomValidationRule(string propertyName, Func<bool> function, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.Function = function;
            this.ErrorMessage = errorMessage;
        }

        public static IList<CustomValidationRule> Create(IList<string> propertyNames, Func<bool> function, string errorMessage)
        {
            IList<CustomValidationRule> rules = new List<CustomValidationRule>();
            foreach (string propertyName in propertyNames)
            {
                rules.Add(new CustomValidationRule(propertyName, function, errorMessage));
            }
            return rules;
        }

        public override string Check()
        {
            // Attention, la fonction retourne true en cas d'erreur (donc si les conditions définies sont respectées)
            if (Function.Invoke())
            {
                return this.ErrorMessage;
            }
            else
            {
                return null;
            }
        }
    }
}
