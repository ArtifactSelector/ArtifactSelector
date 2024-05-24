namespace ArtifactSelector
{
    public static class ErrorMessages
    {
        // Parsing exceptions
        public const string ExpectedBooleanFunction = "Expected boolean functions.";
        public const string SyntaxUnmatchedBracket = "Syntax Error: Unmatched bracket.";
        public const string SyntaxErrorInBooleanExpression = "Syntax Error in boolean expression.";
        public const string ReferencedBeforeDeclared = "Error: {0} is referenced before declared.";
        public const string UnexpectedToken = "Unexpected Token: Expected {0} but got {1} instead.";
        public const string UnexpectedTokenInBooleanExpression = "Unexpected Token: {0} is unexpected in boolean expression.";
        public const string InvalidSubstatParameter = "Invalid Substat Parameter: {0}.";
        public const string InvalidMainstatParameter = "Invalid Mainstat Parameter: {0}.";
        public const string InvalidStatParameter = "Invalid Stat Parameter: {0}.";
        public const string InvalidSetParameter = "Invalid Set Parameter: {0}.";
        public const string ExpectedActionKeyword = "Expected action keyword but got {0}.";
        public const string ExpectedMoreTokens = "Syntax Error: Expected more Tokens.";
        public const string UnrecognizedToken = "Unrecognized token: {0}.";
        public const string ExpectedBooleanExpression = "Expected boolean expression.";

        // Test parsing exception
        public const string ParsingError = "Parsing Error in test source.";
        public const string MouseMoved = "Mouse moved, execution aborted.";

        // Scanner exceptions
        public const string CouldNotRecognizeWord = "Could not recognize scanned word: {0}.";
        public const string ReturnedNoneAction = "No action is returned from the artifact.";

        // Startup errors
        public const string Usage = "Usage: ArtifactSelector.exe <sourcefile> [-t <testfile>]";
        public const string GameWidthZero = "Genshin's window width cannot be 0.";
        public const string GameHeightZero = "Genshin's window height cannot be 0.";
        public const string GameNotFocused = "Genshin window could not be focused. Please make sure the game is visible.";
        public const string GameNotFound = "Cannot find Genshin Impact process.";
        public const string CouldNotReadFile = "Could not read file: {0}.";
        public const string FileDoesNotExist = "File does not exist: {0}.";

    }
}
