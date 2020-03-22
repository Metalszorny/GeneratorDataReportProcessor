using System;
using System.IO;

namespace Common.Exceptions
{
    /// <summary>
    /// Interaction logic for the input folder not exists exception.
    /// </summary>
    public sealed class InputFolderNotExistsException : IOException
    {
        #region Fields

        // The constant message for the exception.
        private const string exceptionMessage = "The given input folder doesn't exist or the application doesn't have permission to access it.";

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Creates an instance of the <see cref="InputFolderNotExistsException"/> class.
        /// </summary>
        public InputFolderNotExistsException()
            : base(exceptionMessage)
        { }

        /// <summary>
        /// Creates an instance of the <see cref="InputFolderNotExistsException"/> class.
        /// </summary>
        /// <param name="exception">The inner exception.</param>
        public InputFolderNotExistsException(Exception exception)
            : base(exceptionMessage, exception)
        { }

        #endregion Constructors
    }
}
