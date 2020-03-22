using System;
using System.IO;

namespace Common.Exceptions
{
    /// <summary>
    /// Interaction logic for the reference folder not exists exception.
    /// </summary>
    public sealed class ReferenceFolderNotExistsException : IOException
    {
        #region Fields

        // The constant message for the exception.
        private const string exceptionMessage = "The given reference folder doesn't exist or the application doesn't have permission to access it.";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFolderNotExistsException"/> class.
        /// </summary>
        public ReferenceFolderNotExistsException()
            : base(exceptionMessage)
        { }

        /// <summary>
        /// Creates an instance of the <see cref="ReferenceFolderNotExistsException"/> class.
        /// </summary>
        /// <param name="exception">The inner exception.</param>
        public ReferenceFolderNotExistsException(Exception exception)
            : base(exceptionMessage, exception)
        { }

        #endregion Constructors
    }
}
