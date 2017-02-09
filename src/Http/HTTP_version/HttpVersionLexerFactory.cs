using System;
using Http.HTTP_name;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Http.HTTP_version
{
    public sealed class HttpVersionLexerFactory : RuleLexerFactory<HttpVersion>
    {
        static HttpVersionLexerFactory()
        {
            Default = new HttpVersionLexerFactory(
                HTTP_name.HttpNameLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton());
        }

        public HttpVersionLexerFactory(
            [NotNull] ILexerFactory<HttpName> httpNameLexerFactory,
            [NotNull] ILexerFactory<Digit> digitLexerFactory)
        {
            if (httpNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(httpNameLexerFactory));
            }
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            HttpNameLexerFactory = httpNameLexerFactory;
            DigitLexerFactory = digitLexerFactory;
        }

        [NotNull]
        public static HttpVersionLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<HttpName> HttpNameLexerFactory { get; set; }

        public override ILexer<HttpVersion> Create()
        {
            var digit = DigitLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                HttpNameLexerFactory.Create(),
                Terminal.Create(@"/", StringComparer.Ordinal),
                digit,
                Terminal.Create(@".", StringComparer.Ordinal),
                digit);
            return new HttpVersionLexer(innerLexer);
        }
    }
}
