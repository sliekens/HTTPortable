using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Http.status_code
{
    public sealed class StatusCodeLexerFactory : RuleLexerFactory<StatusCode>
    {
        static StatusCodeLexerFactory()
        {
            Default = new StatusCodeLexerFactory(Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public StatusCodeLexerFactory([NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static StatusCodeLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; }

        public override ILexer<StatusCode> Create()
        {
            var innerLexer = Repetition.Create(DigitLexerFactory.Create(), 3, 3);
            return new StatusCodeLexer(innerLexer);
        }
    }
}
