namespace Http.Grammar
{
    using System;

    using TextFx;

    public class BadWhiteSpaceLexerFactory : ILexerFactory<BadWhiteSpace>
    {
        private readonly ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory;

        public BadWhiteSpaceLexerFactory(ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory)
        {
            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }

            this.optionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
        }

        public ILexer<BadWhiteSpace> Create()
        {
            var innerLexer = this.optionalWhiteSpaceLexerFactory.Create();
            return new BadWhiteSpaceLexer(innerLexer);
        }
    }
}