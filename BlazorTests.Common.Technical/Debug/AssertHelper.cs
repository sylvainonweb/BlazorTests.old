using System;
using System.Globalization;

namespace BlazorTests.Common.Technical.Debug
{
    /// <summary>
    /// Assertion utility methods that simplify things such as argument checks.
    /// </summary>
    /// <remarks></remarks>
    public static class AssertHelper
    {
        /// <summary>
        /// Checks that argument isn't null. if null, throw an <see cref="System.ArgumentNullException"/> 
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">The name.</param>
        public static void ArgumentNotNull(object argument, string name)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture, "Argument '{0}' cannot be null.", name));
            }
        }

        /// <summary>
        /// Checks that argument isn't null. if null, throw an <see cref="System.ArgumentNullException"/> 
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public static void ArgumentNotNull(object argument, string name, string message)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(name, message);
            }
        }

        /// <summary>
        /// Checks the value of the supplied string and throws an <see cref="System.ArgumentNullException"/> 
        /// if it is <see langword="null"/> or contains only whitespace character(s).
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">The name.</param>
        public static void ArgumentHasText(string argument, string name)
        {
            if (String.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(name, string.Format(CultureInfo.InvariantCulture, "Argument '{0}' cannot be null or resolve to an empty string : '{1}'.", name, argument));
            }
        }

        /// <summary>
        /// Checks the value of the supplied string and throws an <see cref="System.ArgumentNullException"/> 
        /// if it is <see langword="null"/> or contains only whitespace character(s).
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public static void ArgumentHasText(string argument, string name, string message)
        {
            if (String.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentNullException(name, message);
            }
        }

        /// <summary>
        /// Checks that object isn't null. if null, throw an <see cref="System.NullReferenceException"/> 
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="name">The name.</param>
        public static void ObjectNotNull(object o, string name)
        {
            if (o == null)
            {
                throw new NullReferenceException(string.Format(CultureInfo.InvariantCulture, "Object '{0}' cannot be null.", name));
            }
        }

        /// <summary>
        /// Checks that object isn't null. if null, throw an <see cref="System.NullReferenceException"/> 
        /// </summary>
        /// <param name="o">The o.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public static void ObjectNotNull(object o, string name, string message)
        {
            if (o == null)
            {
                throw new NullReferenceException(message);
            }
        }

        /// <summary>
        /// Checks the value of the supplied string and throws an <see cref="System.NullReferenceException"/> 
        /// if it is <see langword="null"/> or contains only whitespace character(s).
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="name">The name.</param>
        public static void StringHasText(string s, string name)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                throw new NullReferenceException(string.Format(CultureInfo.InvariantCulture, "String '{0}' cannot be null or resolve to an empty string : '{1}'.", name, s));
            }
        }

        /// <summary>
        /// Checks the value of the supplied string and throws an <see cref="System.NullReferenceException"/> 
        /// if it is <see langword="null"/> or contains only whitespace character(s).
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="name">The name.</param>
        /// <param name="message">The message.</param>
        public static void StringHasText(string s, string name, string message)
        {
            if (String.IsNullOrWhiteSpace(s))
            {
                throw new NullReferenceException(message);
            }
        }

        /// <summary>
        /// Determines whether the specified condition is true.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message</param>
        public static void IsTrue(bool condition, string message)
        {
            if (condition == false)
            {
                throw new ApplicationException(message);
            }
        }

        /// <summary>
        /// Determines whether the specified condition is true.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message</param>
        public static void IsFalse(bool condition, string message)
        {
            if (condition == true)
            {
                throw new ApplicationException(message);
            }
        }

        #region "Constructor (s) / Destructor"

        #endregion
    }
}
