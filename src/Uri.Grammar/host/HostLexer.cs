namespace Uri.Grammar
{
    using System;

    using SLANG;

    public class HostLexer : Lexer<Host>
    {
        private readonly ILexer<Alternative> innerLexer;

        public HostLexer(ILexer<Alternative> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, out Host element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new Host(result);
                return true;
            }

            element = default(Host);
            return false;
        }
    }
}