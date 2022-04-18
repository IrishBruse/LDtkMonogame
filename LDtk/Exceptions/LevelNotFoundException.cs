namespace LDtk.Exceptions;

using System;

/// <summary>
/// Level Not Found Exception
/// </summary>
public class LevelNotFoundException : Exception
{
    /// <summary>
    /// Level Not Found Exception
    /// </summary>
    public LevelNotFoundException() : base() { }
    /// <summary>
    /// Level Not Found Exception
    /// </summary>
    /// <param name="message"></param>
    public LevelNotFoundException(string message) : base(message) { }

    /// <summary>
    /// Level Not Found Exception
    /// </summary>
    /// <param name="message"></param>
    /// <param name="innerException"></param>
    public LevelNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}
