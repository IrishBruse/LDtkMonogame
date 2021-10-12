using System;

namespace LDtk
{
    class LevelNotFoundException : Exception
    {
        public LevelNotFoundException() : base() { }
        public LevelNotFoundException(string message) : base(message) { }
        public LevelNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}