namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;

    public class UniformResourceIdentifierLexer : Lexer<UniformResourceIdentifier>
    {
        private readonly ILexer<Sequence> innerLexer;

        public UniformResourceIdentifierLexer(ILexer<Sequence> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException("innerLexer");
            }

            this.innerLexer = innerLexer;
        }

        public override bool TryRead(ITextScanner scanner, Element previousElementOrNull, out UniformResourceIdentifier element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, null, out result))
            {
                element = new UniformResourceIdentifier(result);
                if (previousElementOrNull != null)
                {
                    previousElementOrNull.NextElement = element;
                    element.PreviousElement = previousElementOrNull;
                }

                return true;
            }

            element = default(UniformResourceIdentifier);
            return false;
        }
    }
}