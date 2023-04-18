namespace Core
{
    public class Token
    {
        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }
        public int LineNo { get; private set; }

        public Token(TokenType tokenType, string value, int lineNo)
        {
            TokenType = tokenType;
            Value = value;
            LineNo = lineNo;
        }

        public override string ToString()
        {
            return $"line {LineNo}: {TokenType} {Value}";
        }
    }

    public enum TokenType
    {
        LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
        COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,

        BANG, BANG_EQUAL,
        EQUAL, EQUAL_EQUAL,
        GREATER, GREATER_EQUAL,
        LESS, LESS_EQUAL,

        IDENTIFIER, STRING, NUMBER,

        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
        PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE,

        EOF
    }
}
