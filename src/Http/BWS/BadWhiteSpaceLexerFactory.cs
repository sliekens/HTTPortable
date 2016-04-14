using System;
using Http.OWS;
using Txt;

namespace Http.BWS
{
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
            var innerLexer = optionalWhiteSpaceLexerFactory.Create();
            return new BadWhiteSpaceLexer(innerLexer);
        }
    }
}