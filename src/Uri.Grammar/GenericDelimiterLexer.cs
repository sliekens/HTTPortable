

namespace Uri.Grammar
{
    using SLANG;

    public class GenericDelimiterLexer : Lexer<GenericDelimiter>
    {
        public GenericDelimiterLexer()
            : base("gen-delims")
        {
        }

        public override bool TryRead(ITextScanner scanner, out GenericDelimiter element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] { ':', '/', '?', '#', '[', ']', '@' })
            {
                if (scanner.TryMatch(c))
                {
                    element = new GenericDelimiter(c, context);
                    return true;
                }
            }

            return Default(out element);
        }
    }
}