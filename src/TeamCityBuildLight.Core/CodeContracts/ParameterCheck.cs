using System.Collections.Generic;
using System.Linq;

namespace TeamCityBuildLight.Core.CodeContracts
{
    /// <summary>
    /// Precondition checking for methods.
    /// </summary>
    public static class ParameterCheck
    {
        private static string GetParameterRequiredErrorMessage(string parameterName)
        {
            return string.Format("The parameter {0} is required", parameterName);
        }

        private static void ParameterNameRequired(string parameterName)
        {
            Require(!string.IsNullOrEmpty(parameterName), GetParameterRequiredErrorMessage("parameterName"));
        }

        /// <summary>
        /// Throw a Precondition exception if the provided string
        /// is null, empty or contains only whitespace.
        /// </summary>
        /// <param name="parameter">Teh string parameter to check.</param>
        /// <param name="parameterName">The name of the string parameter (used in error messages).</param>
        /// <exception cref="PreconditionException">
        /// <para>The parameter is null, empty or contains only whitespace.</para>
        /// <para>- or -</para>
        /// <para>The parameter name is null, empty or all whitespace</para>
        /// </exception>
        public static void StringRequiredAndNotWhitespace(string parameter, string parameterName)
        {
            ParameterNameRequired(parameterName);

            Require(!string.IsNullOrWhiteSpace(parameter), string.Format("The string parameter {0} must not be null, empty or contain only whitespace", parameterName));
        }

        /// <summary>
        /// Throws a Precondition exception if the provided object
        /// is null
        /// </summary>
        /// <param name="parameter">The object to test.</param>
        /// <param name="parameterName">The name of the parameter.</param>
        /// <exception cref="PreconditionException">
        /// <para>The object is null</para>
        /// <para>- or -</para>
        /// <para>The parameter name is null, empty or all whitespace</para>
        /// </exception>
        public static void ParameterRequired(object parameter, string parameterName)
        {
            ParameterNameRequired(parameterName);

            Require(parameter != null, GetParameterRequiredErrorMessage(parameterName));
        }

        private static void Require(bool assertion, string message)
        {
            if (!assertion) throw new PreconditionException(message);
        }
    }
}