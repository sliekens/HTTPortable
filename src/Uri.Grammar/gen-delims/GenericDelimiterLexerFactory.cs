namespace Uri.Grammar.gen_delims
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            var genericDelimiterAlternativeLexer = this.alternativeLexerFactory.Create(
                this.stringLexerFactory.Create(@":"),
                this.stringLexerFactory.Create(@"/"),
                this.stringLexerFactory.Create(@"?"),
                this.stringLexerFactory.Create(@"#"),
                this.stringLexerFactory.Create(@"["),
                this.stringLexerFactory.Create(@"]"),
                this.stringLexerFactory.Create(@"@"));
            return new GenericDelimiterLexer(genericDelimiterAlternativeLexer);
        }
    }
}
