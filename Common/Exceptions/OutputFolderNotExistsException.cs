using System;
using System.IO;

namespace Common.Exceptions
{
    /// <summary>
    /// Interaction logic for the output folder not exists exception.
    /// </summary>
    public sealed class OutputFolderNotExistsException : IOException
    {
        #region Fields

        // The constant message for the exception.
        private const string exceptionMessage = "The given output folder doesn't exist or the application doesn't have permission to access it.";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="OutputFolderNotExistsException"/> class.
        /// </summary>
        public OutputFolderNotExistsException()
            : base(exceptionMessage)
        { }

        /// <summary>
        /// Creates an instance of the <see cref="OutputFolderNotExistsException"/> class.
        /// </summary>
        /// <param name="exception">The inner exception.</param>
        public OutputFolderNotExistsException(Exception exception)
            : base(exceptionMessage, exception)
        { }

        #endregion Constructors
    }
}
