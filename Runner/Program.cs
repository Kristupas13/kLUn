using Core;
using System.Text;

namespace Interpreter
{
    internal class Program
    {
        private const string PATH = "C:\\Users\\kristupas.lunskas\\Desktop\\Training\\Interpreter\\Core\\example.kl";
        static int Main(string[] args)
        {
            var text = File.ReadAllText(PATH, Encoding.UTF8);

            var lexer = new Lexer(text);
            var tokens = lexer.ScanTokens();
            if (ErrorHandler.ErrorOccurred)
            {
                return -1; 
            }

            PrintTokens(tokens);

            return 0;
        }

        static void PrintTokens(IEnumerable<Token> tokens)
        {
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}