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

        public override bool TryRead(ITextScanner scanner, out UniformResourceIdentifier element)
        {
            Sequence result;
            if (this.innerLexer.TryRead(scanner, out result))
            {
                element = new UniformResourceIdentifier(result);
                return true;
            }

            element = default(UniformResourceIdentifier);
            return false;
        }
    }
}