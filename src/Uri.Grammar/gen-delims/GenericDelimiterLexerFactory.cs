namespace Uri.Grammar.gen_delims
{
    using System;

    using SLANG;

    public class GenericDelimiterLexerFactory : ILexerFactory<GenericDelimiter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public GenericDelimiterLexerFactory(IStringLexerFactory stringLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
        {
            if (stringLexerFactory == null)
            {
                throw new ArgumentNullException("stringLexerFactory", "Precondition: stringLexerFactory != null");
            }

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException("alternativeLexerFactory", "Precondition: alternativeLexerFactory != null");
            }

            this.stringLexerFactory = stringLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<GenericDelimiter> Create()
        {
            ILexer[] a =
                {
                    this.stringLexerFactory.Create(@":"),
                    this.stringLexerFactory.Create(@"/"),
                    this.stringLexerFactory.Create(@"?"),
                    this.stringLexerFactory.Create(@"#"),
                    this.stringLexerFactory.Create(@"["),
                    this.stringLexerFactory.Create(@"]"),
                    this.stringLexerFactory.Create(@"@")
                };

            // ":" / "/" / "?" / "#" / "[" / "]" / "@"
            var b = this.alternativeLexerFactory.Create(a);

            // gen-delims
            return new GenericDelimiterLexer(b);
        }
    }
}
