using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.WSP;

namespace Http.OWS
{
    public class OptionalWhiteSpaceLexerFactory : ILexerFactory<OptionalWhiteSpace>
    {
        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<WhiteSpace> whiteSpaceLexer;

        public OptionalWhiteSpaceLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<WhiteSpace> whiteSpaceLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (whiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.whiteSpaceLexer = whiteSpaceLexer;
        }

        public ILexer<OptionalWhiteSpace> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(whiteSpaceLexer, 0, int.MaxValue);
            return new OptionalWhiteSpaceLexer(innerLexer);
        }
    }
}
