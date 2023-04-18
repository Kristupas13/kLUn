namespace Core
{
    public static class ErrorHandler
    {
        public static bool ErrorOccurred = false;
        public static void Error(int line, string prefix, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{prefix}_error:line {line}: {message}");
            Console.ForegroundColor = ConsoleColor.White;

            ErrorOccurred = true;
        }
    }
}
