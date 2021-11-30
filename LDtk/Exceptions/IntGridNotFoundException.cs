using System;

namespace LDtk.Exceptions
{
    /// <summary>
    /// IntGrid NotFound Exception
    /// </summary>
    [Serializable]
    public class IntGridNotFoundException : Exception
    {
        /// <summary>
        /// IntGrid NotFound Exception
        /// </summary>
        public IntGridNotFoundException() { }
        /// <summary>
        /// IntGrid NotFound Exception
        /// </summary>
        public IntGridNotFoundException(string message) : base(message) { }
        /// <summary>
        /// IntGrid NotFound Exception
        /// </summary>
        public IntGridNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
