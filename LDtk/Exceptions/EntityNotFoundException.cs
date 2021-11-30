using System;

namespace LDtk.Exceptions
{
    /// <summary>
    /// Entity Not Found Exception
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        /// <summary>
        /// Entity Not Found Exception
        /// </summary>
        public EntityNotFoundException() : base() { }

        /// <summary>
        /// Entity Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        public EntityNotFoundException(string message) : base(message) { }

        /// <summary>
        /// Entity Not Found Exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public EntityNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
