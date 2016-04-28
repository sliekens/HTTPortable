using System;
using Http.HTTP_name;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.DIGIT;

namespace Http.HTTP_version
{
    public class HttpVersionLexerFactory : ILexerFactory<HttpVersion>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly ILexer<HttpName> httpNameLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpVersionLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<HttpName> httpNameLexer,
            [NotNull] ILexer<Digit> digitLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (httpNameLexer == null)
            {
                throw new ArgumentNullException(nameof(httpNameLexer));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.httpNameLexer = httpNameLexer;
            this.digitLexer = digitLexer;
        }

        public ILexer<HttpVersion> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                httpNameLexer,
                terminalLexerFactory.Create(@"/", StringComparer.Ordinal),
                digitLexer,
                terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                digitLexer);
            return new HttpVersionLexer(innerLexer);
        }
    }
}
