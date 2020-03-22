using System;

namespace Common.Exceptions
{
    /// <summary>
    /// Interaction logic for the reference file serialization exception.
    /// </summary>
    public sealed class ReferenceFileSerializationException : ApplicationException
    {
        #region Fields

        // The constant message for the exception.
        private const string exceptionMessage = "The reference data file could not be serialized.";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFileSerializationException"/> class.
        /// </summary>
        public ReferenceFileSerializationException()
            : base(exceptionMessage)
        { }

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFileSerializationException"/> class.
        /// </summary>
        /// <param name="exception">The inner exception.</param>
        public ReferenceFileSerializationException(Exception exception)
            : base(exceptionMessage, exception)
        { }

        #endregion Constructors
    }
}
