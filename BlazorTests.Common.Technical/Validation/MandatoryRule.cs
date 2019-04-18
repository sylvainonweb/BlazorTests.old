using System.Collections;
using System.Security;

namespace BlazorTests.Common.Technical.Validation
{
    public class MandatoryRule : ValidationRule
    {
        public string PropertyText { get; set; }
        public object Value { get; set; }
        public string ErrorMessage { get; set; }

        public MandatoryRule(string propertyName, string propertyText, object value, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.PropertyText = propertyText;
            this.Value = value;
            this.ErrorMessage = errorMessage;
        }

        public override string Check()
        {
            if (Value == null)
            {
                return GetErrorMessage(false);
            }
            else
            {
                // Dans le cas d'une chaîne, on vérifie aussi qu'elle n'est pas vide
                if (Value is string && string.IsNullOrWhiteSpace((string)Value))
                {
                    return GetErrorMessage(false);
                }

                // Dans le cas d'une SecureString, on vérifie aussi qu'elle n'est pas vide
                if (Value is SecureString && ((SecureString)Value).Length == 0)
                {
                    return GetErrorMessage(false);
                }

                // Dans le cas d'une liste, on vérifie aussi qu'elle contient un élément
                if (Value is IList && ((IList)Value).Count == 0)
                {
                    return GetErrorMessage(true);
                }
            }

            return null;
        }

        private string GetErrorMessage(bool useListErrorMessage)
        {
            // En 1er lieu, on prend le message d'erreur défini par le développeur
            if (!string.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                return ErrorMessage;
            }

            // Sinon, on prend le message d'erreur par défaut
            if (useListErrorMessage)
            {
                return string.Format("Veuillez sélectionner au moins un élement de la liste {0}", PropertyText);
            }
            else
            {
                return string.Format("Veuillez renseigner le champ {0}", PropertyText);
            }
        }
    }
}
