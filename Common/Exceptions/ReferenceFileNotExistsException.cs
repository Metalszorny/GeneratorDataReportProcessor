using System;
using System.IO;

namespace Common.Exceptions
{
    /// <summary>
    /// Interaction logic for the reference file not exists exception.
    /// </summary>
    public sealed class ReferenceFileNotExistsException : IOException
    {
        #region Fields

        // The constant message for the exception.
        private const string exceptionMessage = "The given reference file doesn't exist or the application doesn't have permission to access it.";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFileNotExistsException"/> class.
        /// </summary>
        public ReferenceFileNotExistsException()
            : base(exceptionMessage)
        { }

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFileNotExistsException"/> class.
        /// </summary>
        /// <param name="exception">The inner exception.</param>
        public ReferenceFileNotExistsException(Exception exception)
            : base(exceptionMessage, exception)
        { }

        #endregion Constructors
    }
}
