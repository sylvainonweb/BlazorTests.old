using BlazorTests.Common.Data.Enumerations;
using BlazorTests.Common.Technical.Property;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorTests.Common.Technical.Attribute
{
    public static class AttributeHelper
    {
        #region Via Lambda Expression

        public static object[] GetCustomAttributes<T>(Expression<Func<T>> propertyLambda, Type attributeType)
        {
            PropertyInfo info = PropertyHelper.GetPropertyInfo(propertyLambda);
            return attributeType == null ? info.GetCustomAttributes(true) : info.GetCustomAttributes(attributeType, true);
        }

        public static object GetCustomAttribute<T>(Expression<Func<T>> propertyLambda, Type attributeType)
        {
            PropertyInfo info = PropertyHelper.GetPropertyInfo(propertyLambda);
            return info.GetCustomAttribute(attributeType, true);
        }

        public static object GetCustomAttributeValue<T>(Expression<Func<T>> propertyLambda, Type attributeType, string propertyName)
        {
            object attribute = GetCustomAttribute(propertyLambda, attributeType);
            PropertyInfo info = attributeType.GetProperty(propertyName);
            return info.GetValue(attribute);
        }

        #endregion

        #region Via Reflection

        public static bool HasAttribute<T>(MethodInfo method, bool inherit) where T:System.Attribute
        {
            object[] foundAttributes = method.GetCustomAttributes(typeof(T), inherit);
            return foundAttributes.Length > 0;
        }

        public static bool HasAttribute<T>(Type classType, bool inherit) where T : System.Attribute
        {
            object[] foundAttributes = classType.GetCustomAttributes(typeof(T), inherit);
            return foundAttributes.Length > 0;
        }

        public static bool HasAttribute<T>(PropertyInfo propertyInfo, bool inherit) where T : System.Attribute
        {
            object[] foundAttributes = propertyInfo.GetCustomAttributes(typeof(T), inherit);
            return foundAttributes.Length > 0;
        }

        public static T GetCustomAttribute<T>(PropertyInfo propertyInfo, bool inherit) where T : System.Attribute
        {
            object[] foundAttributes = propertyInfo.GetCustomAttributes(typeof(T), inherit);
            if (foundAttributes.Length > 0)
            {
                return foundAttributes[0] as T;
            }
            return null;
        }

        

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the [EnumStringValue] attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetText(System.Enum value)
        {
            System.Reflection.FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            EnumStringValueAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(EnumStringValueAttribute), false) as EnumStringValueAttribute[];
            return attributes.Length > 0 ? attributes[0].Text : null;
        }

        #endregion
    }
}
