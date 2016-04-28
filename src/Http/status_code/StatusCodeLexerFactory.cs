using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Http.status_code
{
    public class StatusCodeLexerFactory : ILexerFactory<StatusCode>
    {
        private readonly ILexer<Digit> digitLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public StatusCodeLexerFactory(
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexer = digitLexer;
        }

        public ILexer<StatusCode> Create()
        {
            var innerLexer = repetitionLexerFactory.Create(digitLexer, 3, 3);
            return new StatusCodeLexer(innerLexer);
        }
    }
}
