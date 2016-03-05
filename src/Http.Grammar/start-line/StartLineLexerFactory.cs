namespace Http.Grammar
{
    using System;
    using TextFx;
    using TextFx.ABNF;

    public class StartLineLexerFactory : ILexerFactory<StartLine>
    {
        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<RequestLine> requestLineLexerFactory;

        private readonly ILexerFactory<StatusLine> statusLineLexerFactory;

        public StartLineLexerFactory(
            IAlternativeLexerFactory alternativeLexerFactory,
            ILexerFactory<RequestLine> requestLineLexerFactory,
            ILexerFactory<StatusLine> statusLineLexerFactory)
        {
            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }
            if (requestLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requestLineLexerFactory));
            }
            if (statusLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(statusLineLexerFactory));
            }
            this.alternativeLexerFactory = alternativeLexerFactory;
            this.requestLineLexerFactory = requestLineLexerFactory;
            this.statusLineLexerFactory = statusLineLexerFactory;
        }

        public ILexer<StartLine> Create()
        {
            return
                new StartLineLexer(
                    alternativeLexerFactory.Create(requestLineLexerFactory.Create(), statusLineLexerFactory.Create()));
        }
    }
}
