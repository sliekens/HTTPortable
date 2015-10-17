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

        private readonly ISequenceLexerFactory sequenceLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpVersionLexerFactory(
            ISequenceLexerFactory sequenceLexerFactory,
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<HttpName> httpNameLexerFactory,
            ILexerFactory<Digit> digitLexerFactory)
        {
            if (sequenceLexerFactory == null)
            {
                throw new ArgumentNullException("sequenceLexerFactory");
            }

            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException("terminalLexerFactory");
            }

            if (httpNameLexerFactory == null)
            {
                throw new ArgumentNullException("httpNameLexerFactory");
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException("digitLexerFactory");
            }

            this.sequenceLexerFactory = sequenceLexerFactory;
            this.terminalLexerFactory = terminalLexerFactory;
            this.httpNameLexerFactory = httpNameLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
        }

        public ILexer<HttpVersion> Create()
        {
            var httpName = this.httpNameLexerFactory.Create();
            var digit = this.digitLexerFactory.Create();
            var slash = this.terminalLexerFactory.Create(@"/");
            var dot = this.terminalLexerFactory.Create(@".");
            var innerLexer = this.sequenceLexerFactory.Create(httpName, slash, digit, dot, digit);
            return new HttpVersionLexer(innerLexer);
        }
    }
}