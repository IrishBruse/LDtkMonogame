namespace LDtk;

using System;

/// <summary>
/// Generic LDtk Exception.
/// </summary>
public class LDtkException : Exception
{
    /// <summary> Initializes a new instance of the <see cref="LDtkException"/> class. </summary>
    /// <param name="message"> The message. </param>
    public LDtkException(string message)
        : base(message)
    {
    }

    /// <summary> Initializes a new instance of the <see cref="LDtkException"/> class. </summary>
    LDtkException()
    {
    }

    /// <summary> LDtk Exception </summary>
    public LDtkException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
