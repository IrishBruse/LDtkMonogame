namespace LDtk.Exceptions
{
    /// <summary>
    /// Unknown FieldInstance Exception
    /// </summary>
    [System.Serializable]
    public class DefinitionException : System.Exception
    {
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public DefinitionException() { }
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public DefinitionException(string message) : base(message) { }
        /// <summary>
        /// Unknown FieldInstance Exception
        /// </summary>
        public DefinitionException(string message, System.Exception inner) : base(message, inner) { }
    }
}
