namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

    public class HttpVersionLexerFactory : ILexerFactory<HttpVersion>
    {
        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ILexerFactory<HttpName> httpNameLexerFactory;

        private readonly IConcatenationLexerFactory ConcatenationLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpVersionLexerFactory(
            IConcatenationLexerFactory ConcatenationLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<HttpName> httpNameLexerFactory,
            ILexerFactory<Digit> digitLexerFactory)
        {
            if (ConcatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(ConcatenationLexerFactory));
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (httpNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(httpNameLexerFactory));
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }

            this.ConcatenationLexerFactory = ConcatenationLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.httpNameLexerFactory = httpNameLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<HttpVersion> Create()
        {
            var httpName = this.httpNameLexerFactory.Create();
            var digit = this.digitLexerFactory.Create();
            var slash = this.terminalLexerFactory.Create(@"/", StringComparer.Ordinal);
            var dot = this.terminalLexerFactory.Create(@".", StringComparer.Ordinal);
            var innerLexer = this.ConcatenationLexerFactory.Create(httpName, slash, digit, dot, digit);
            return new HttpVersionLexer(innerLexer);
        }
    }
}