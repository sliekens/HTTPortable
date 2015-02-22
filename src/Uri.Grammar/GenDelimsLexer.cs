using Text.Scanning;

namespace Uri.Grammar
{
    public class GenDelimsLexer : Lexer<GenDelims>
    {
        public override GenDelims Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            GenDelims element;
            if (TryRead(scanner, out element))
            {
                return element;
            }

            throw new SyntaxErrorException(context, "Expected 'gen-delims'");
        }

        public override bool TryRead(ITextScanner scanner, out GenDelims element)
        {
            if (scanner.EndOfInput)
            {
                return Default(out element);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] {':', '/', '?', '#', '[', ']', '@'})
            {
                if (scanner.TryMatch(c))
                {
                    element = new GenDelims(c, context);
                    return true;
                }
            }

            return Default(out element);
        }
    }
}