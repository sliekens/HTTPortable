using System;
using Http.request_line;
using Http.status_line;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.start_line
{
    public class StartLineLexerFactory : ILexerFactory<StartLine>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<RequestLine> requestLineLexer;

        private readonly ILexer<StatusLine> statusLineLexer;

        public StartLineLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<RequestLine> requestLineLexer,
            [NotNull] ILexer<StatusLine> statusLineLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (requestLineLexer == null)
            {
                throw new ArgumentNullException(nameof(requestLineLexer));
            }
            if (statusLineLexer == null)
            {
                throw new ArgumentNullException(nameof(statusLineLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.requestLineLexer = requestLineLexer;
            this.statusLineLexer = statusLineLexer;
        }

        public ILexer<StartLine> Create()
        {
            var innerLexer = alternationLexerFactory.Create(requestLineLexer, statusLineLexer);
            return new StartLineLexer(innerLexer);
        }
    }
}
