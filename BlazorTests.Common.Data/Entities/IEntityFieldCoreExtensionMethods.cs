using System;

namespace SD.LLBLGen.Pro.ORMSupportClasses
{
    public static class IEntityFieldCoreExtensionMethods
    {
        public static bool IsChangedEx(this IEntityFieldCore entityFieldCore)
        {
            if (entityFieldCore.IsChanged)
            {
                if (entityFieldCore.DbValue == null)
                {
                    // Ne devrait plus arrivé car ce cas est à présent géré par CommonEntityBase.PreProcessValueToSet
                    // 1er cas : Valeur de type chaine null ou vide
                    if (entityFieldCore.DataType == typeof(string))
                    {
                        string s = entityFieldCore.CurrentValue as string;
                        if (string.IsNullOrEmpty(s))
                        {
                            // La valeur stockée en base est null et la valeur courante est une chaine vide ("")
                            // => identique
                            return false;
                        }
                    }
                }
                else
                {
                    string currentValueString = GetString(entityFieldCore.CurrentValue);

                    // 2ème cas : Valeur de type chaine identique
                    if (entityFieldCore.DataType == typeof(string))
                    {
                        string dbValueString = GetString(entityFieldCore.DbValue);
                        if (currentValueString.Equals(dbValueString))
                        {
                            return false;
                        }
                    }

                    // 3ème cas : Valeur double
                    if (IsNumeric(currentValueString))
                    {
                        double currentValueAsDouble = GetDouble(currentValueString);

                        string dbValueString = GetString(entityFieldCore.DbValue);
                        double dbValueAsDouble = GetDouble(dbValueString);

                        const int NumberOfDecimals = 10;
                        if (Math.Round(currentValueAsDouble, NumberOfDecimals) == Math.Round(dbValueAsDouble, NumberOfDecimals))
                        {
                            // A l'arrondi près, les valeurs sont égales
                            // => identique
                            return false;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        private static string GetString(object o)
        {
            string currentValueString = string.Empty;
            if (o != null)
            {
                currentValueString = o.ToString();
            }
            return currentValueString;
        }

        private static bool IsNumeric(string s)
        {
            double result;
            return double.TryParse(s, out result);
        }

        private static double GetDouble(string s)
        {
            double result;
            double.TryParse(s, out result);
            return result;
        }
    }
}
