using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Http.status_code
{
    public class StatusCodeLexerFactory : ILexerFactory<StatusCode>
    {
        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public StatusCodeLexerFactory(
            IRepetitionLexerFactory repetitionLexerFactory,
            ILexerFactory<Digit> digitLexerFactory)
        {
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }

            this.repetitionLexerFactory = repetitionLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<StatusCode> Create()
        {
            var digit = digitLexerFactory.Create();
            var innerLexer = repetitionLexerFactory.Create(digit, 3, 3);
            return new StatusCodeLexer(innerLexer);
        }
    }
}