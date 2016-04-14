using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.WSP;

namespace Http.OWS
{
    public class OptionalWhiteSpaceLexerFactory : ILexerFactory<OptionalWhiteSpace>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexerFactory<WhiteSpace> whiteSpaceLexerFactory;

        public OptionalWhiteSpaceLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<WhiteSpace> whiteSpaceLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.whiteSpaceLexerFactory = whiteSpaceLexerFactory;
        }

        public ILexer<OptionalWhiteSpace> Create()
        {
            var wsp = whiteSpaceLexerFactory.Create();
            var innerLexer = repetitionLexerFactory.Create(wsp, 0, int.MaxValue);
            return new OptionalWhiteSpaceLexer(innerLexer);
        }
    }
}