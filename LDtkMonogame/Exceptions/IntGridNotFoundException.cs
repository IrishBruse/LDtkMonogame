namespace LDtk.Exceptions
{
    /// <summary>
    /// IntGrid NotFound Exception
    /// </summary>
    [System.Serializable]
    public class IntGridNotFoundException : System.Exception
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
        public IntGridNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// IntGrid NotFound Exception
        /// </summary>
        protected IntGridNotFoundException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}