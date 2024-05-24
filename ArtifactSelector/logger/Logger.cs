using System;

namespace ArtifactSelector
{
    internal class Logger
    {
        public static void Notice(string format, params object[] args)
        {
            string message = string.Format(format, args);
            Console.WriteLine(message);
        }

        public static void Error(string format, params object[] args)
        {
            string message = string.Format(format, args);
            Console.WriteLine(message);
        }
    }
}
