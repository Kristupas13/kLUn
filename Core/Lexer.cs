using System.Text;

namespace Core
{
    public class Lexer
    {

        private List<Token> tokens = new List<Token>();
        
        private string content;
        private int currentLine = 1;
        private int currentElementPos = 0;

        public Lexer(string content)
        {
            this.content = content;
        }

        public List<Token> ScanTokens()
        {
            while(IsTheEnd() == false)
            {
                ScanToken();
            }

            tokens.Add(new Token(TokenType.EOF, "", currentLine));
            return tokens;
        }

        private void ScanToken()
        {
            var currentChar = Advance();
            switch (currentChar)
            {
                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    currentLine++;
                    break;

                case '(': AddToken(TokenType.LEFT_PAREN); break;
                case ')': AddToken(TokenType.RIGHT_PAREN); break;
                case '{': AddToken(TokenType.LEFT_BRACE); break;
                case '}': AddToken(TokenType.RIGHT_BRACE); break;
                case ',': AddToken(TokenType.COMMA); break;
                case '.': AddToken(TokenType.DOT); break;
                case '-': AddToken(TokenType.MINUS); break;
                case '+': AddToken(TokenType.PLUS); break;
                case ';': AddToken(TokenType.SEMICOLON); break;
                case '*': AddToken(TokenType.STAR); break;

                case '!': AddToken(Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
                case '=': AddToken(Match('=') ? TokenType.EQUAL : TokenType.EQUAL); break;
                case '>': AddToken(Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
                case '<': AddToken(Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
                case '/': SlashState(); break;
                case '"': StringState(); break;


                default: ErrorHandler.Error(currentLine, "lexer", $"Unexpected character {currentChar}"); break;
            }
        }

        private void StringState()
        {
            StringBuilder strBuilder = new StringBuilder(string.Empty);
            while (Peek() != '"' && IsTheEnd() == false)
            {
                var current = Peek();

                if (Match('\n'))
                {
                    currentLine++;
                    continue;
                }

                var t = Advance();
                strBuilder.Append(Advance());
            }

            AddToken(TokenType.STRING, strBuilder.ToString());
        }

        private void SlashState()
        {
            if (Peek() == '/')
            {
                while (Peek() != '\n' && IsTheEnd())
                {
                    Advance();
                }
            }
            else
            {
                AddToken(TokenType.SLASH);
            }
        }

        private char Peek()
        {
            return content[currentElementPos + 1];
        }

        private char Advance()
        {
            return content[currentElementPos++];
        }

        private bool Match(char literal)
        {
            if (IsTheEnd())
            {
                return false;
            }
            if (Peek() == literal)
            {
                Advance();
                return true;
            }

            return false;
        }

        private bool IsTheEnd()
        {
            return currentElementPos >= content.Length;
        }

        private void AddToken(TokenType tokenType)
        {
            AddToken(tokenType, string.Empty);
        }

        private void AddToken(TokenType tokenType, string value)
        {
            tokens.Add(new Token(tokenType, value, currentLine));
        }
    }
}