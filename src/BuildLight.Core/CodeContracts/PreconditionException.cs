using System;
using System.Runtime.Serialization;

namespace BuildLight.Core.CodeContracts
{
    /// <summary>
    /// Method preconditions not met.
    /// </summary>
    /// <remarks>
    /// Code should not generally throw this exception.  Rather,
    /// use methods in the static ParameterCheck class
    /// </remarks>
    /// <seealso cref="ParameterCheck"/>
    [Serializable]
    public class PreconditionException : Exception
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public PreconditionException()
        { }

        /// <summary>
        /// Constructor with error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public PreconditionException(string message)
            : base(message)
        { }

        /// <summary>
        /// Constructior with error message and inner exception.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="inner">The inner exception.</param>
        public PreconditionException(string message, Exception inner)
            : base(message, inner)
        { }

        /// <summary>
        /// Constructor used in serialization.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Serialization context.</param>
        protected PreconditionException(SerializationInfo info,
                                        StreamingContext context)
            : base(info, context)
        { }
    }
}