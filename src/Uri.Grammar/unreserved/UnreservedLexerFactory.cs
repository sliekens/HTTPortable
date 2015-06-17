namespace Uri.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class UnreservedLexerFactory : ILexerFactory<Unreserved>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public UnreservedLexerFactory(
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            IStringLexerFactory stringLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException("alphaLexerFactory", "Precondition: alphaLexerFactory != null");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory", "Precondition: digitLexerFactory != null");
            }

            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException("alphaLexerFactory", "Precondition: alphaLexerFactory != null");
            }

            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.stringLexerFactory = stringLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<Unreserved> Create()
        {
            var unreservedAlternativeLexer = this.alternativeLexerFactory.Create(
                this.alphaLexerFactory.Create(),
                this.digitLexerFactory.Create(),
                this.stringLexerFactory.Create(@"-"),
                this.stringLexerFactory.Create(@"."),
                this.stringLexerFactory.Create(@"_"),
                this.stringLexerFactory.Create(@"~"));
            return new UnreservedLexer(unreservedAlternativeLexer);
        }
    }
}