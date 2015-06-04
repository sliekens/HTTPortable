namespace Uri.Grammar.sub_delims
{
    using System;

    using SLANG;

    public class SubcomponentsDelimiterLexerFactory : ILexerFactory<SubcomponentsDelimiter>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly IStringLexerFactory stringLexerFactory;

        public SubcomponentsDelimiterLexerFactory(IStringLexerFactory stringLexerFactory, IAlternativeLexerFactory alternativeLexerFactory)
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

        public ILexer<SubcomponentsDelimiter> Create()
        {
            var alternativeLexer = this.alternativeLexerFactory.Create(
                this.stringLexerFactory.Create(@"!"),
                this.stringLexerFactory.Create(@"$"),
                this.stringLexerFactory.Create(@"&"),
                this.stringLexerFactory.Create(@"'"),
                this.stringLexerFactory.Create(@"("),
                this.stringLexerFactory.Create(@")"),
                this.stringLexerFactory.Create(@"*"),
                this.stringLexerFactory.Create(@"+"),
                this.stringLexerFactory.Create(@","),
                this.stringLexerFactory.Create(@";"),
                this.stringLexerFactory.Create(@"="));
            return new SubcomponentsDelimiterLexer(alternativeLexer);
        }
    }
}
