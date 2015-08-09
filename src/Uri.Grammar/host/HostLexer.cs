namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

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

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out Host element)
        {
            Alternative result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new Host(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(Host);
            return false;
        }
    }
}