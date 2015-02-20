using Text.Scanning;

namespace Uri.Grammar
{
    public class GenDelimsLexer : Lexer<GenDelimsToken>
    {
        public override GenDelimsToken Read(ITextScanner scanner)
        {
            var context = scanner.GetContext();
            GenDelimsToken token;
            if (TryRead(scanner, out token))
            {
                return token;
            }

            throw new SyntaxErrorException(context, "Expected 'gen-delims'");
        }

        public override bool TryRead(ITextScanner scanner, out GenDelimsToken token)
        {
            if (scanner.EndOfInput)
            {
                return Default(out token);
            }

            var context = scanner.GetContext();
            foreach (var c in new[] {':', '/', '?', '#', '[', ']', '@'})
            {
                if (scanner.TryMatch(c))
                {
                    token = new GenDelimsToken(c, context);
                    return true;
                }
            }

            return Default(out token);
        }
    }
}