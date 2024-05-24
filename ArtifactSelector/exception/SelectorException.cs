using System;

namespace ArtifactSelector
{
    public class SelectorException : Exception
    {
        public SelectorException(string message, params object[] args) : base(string.Format(message, args))
        {
        }
    }
}
