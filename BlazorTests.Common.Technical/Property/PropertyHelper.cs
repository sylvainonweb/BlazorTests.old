using BlazorTests.Common.Technical.Debug;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorTests.Common.Technical.Property
{
    public static class PropertyHelper
    {
        public static string GetPropertyName<T>(Expression<Func<T>> propertyLambda)
        {
            return GetPropertyInfo(propertyLambda).Name;
        }

        public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T>> propertyLambda)
        {
            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' doesn't refers to a property.", propertyLambda.ToString()));
            }
            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.", propertyLambda.ToString()));
            }

            return propInfo;
        }

        /// <summary>
        /// Récupère la propriété spécifiée de l'objet spécifié
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetProperty<T>(object item, Expression<Func<T>> propertyLambda)
        {
            PropertyInfo info = GetPropertyInfo(propertyLambda);
            MethodInfo methodInfo = info.GetGetMethod();
            object o = methodInfo.Invoke(item, null);
            if (o == null)
            {
                return default(T);
            }

            return (T)o;
        }

        /// <summary>
        /// Met à jour la propriété spécifiée de l'objet spécifié
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetProperty<T>(object item, Expression<Func<T>> propertyLambda, T value)
        {
            PropertyInfo info = GetPropertyInfo(propertyLambda);
            MethodInfo methodInfo = info.GetSetMethod();
            methodInfo.Invoke(item, new object[1] { value });
        }

        /// <summary>
        /// Retourne le nom de l'élément (variable, propriété) spécifié.
        /// Le but de cette fonction est d'éviter que le code soit truffé de chaines codées en dur.
        /// 
        /// Exemple :
        /// Le code suivant 
        ///     if (e.PropertyName == "MyProperty")
        ///     {
        ///     }
        /// pourra être remplacé par :
        ///     string propertyName = ;
        ///     if (e.PropertyName == ReflectionHelper.GetName(new { MyProperty} ))
        ///     {
        ///     }
        /// 
        /// Note : Cette opération aurait aussi pu être effectué via la fonction suivante mais
        /// elle prend à priori 20 fois plus de temps.
        /// public static string GetName<T>(Expression<Func<T>> expression)
        /// {
        ///     var body = ((MemberExpression)expression.Body);
        ///     return body.Member.Name;
        /// }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetName<T>(T item) where T : class
        {
            return typeof(T).GetProperties()[0].Name;
        }
    }
}
