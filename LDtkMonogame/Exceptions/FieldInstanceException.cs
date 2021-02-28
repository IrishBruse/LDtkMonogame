namespace LDtk.Exceptions
{
    /// <summary>
    /// Unknown FieldInstance Exception
    /// </summary>
    [System.Serializable]
    public class FieldInstanceException : System.Exception
    {
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public FieldInstanceException() { }
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public FieldInstanceException(string message) : base(message) { }
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public FieldInstanceException(string message, System.Exception inner) : base(message, inner) { }
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        protected FieldInstanceException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}