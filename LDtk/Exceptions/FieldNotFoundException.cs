namespace LDtk.Exceptions;

/// <summary>
/// FieldNotFoundException Exception
/// </summary>
[System.Serializable]
public class FieldNotFoundException : System.Exception
{
    /// <summary>
    /// FieldNotFoundException Exception
    /// </summary>
    public FieldNotFoundException() { }

    /// <summary>
    /// FieldNotFoundException Exception
    /// </summary>
    public FieldNotFoundException(string message) : base(message) { }

    /// <summary>
    /// FieldNotFoundException Exception
    /// </summary>
    public FieldNotFoundException(string message, System.Exception inner) : base(message, inner) { }
}
