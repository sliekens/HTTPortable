using Text.Scanning;

namespace Uri.Grammar
{
    public class SubcomponentsDelimiterLexer : Lexer<SubcomponentsDelimiter>
    {
        public SubcomponentsDelimiterLexer()
            : base("sub-delims")
        {
        }

        public override bool TryRead(ITextScanner scanner, out SubcomponentsDelimiter element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '=' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new SubcomponentsDelimiter(c, context);
                    return true;
                }
            }

            return Default(out element);
        }
    }
}