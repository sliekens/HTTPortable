using System;
using Http.request_line;
using Http.status_line;
using Txt;
using Txt.ABNF;

namespace Http.start_line
{
    public class StartLineLexerFactory : ILexerFactory<StartLine>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<RequestLine> requestLineLexerFactory;

        private readonly ILexerFactory<StatusLine> statusLineLexerFactory;

        public StartLineLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            ILexerFactory<RequestLine> requestLineLexerFactory,
            ILexerFactory<StatusLine> statusLineLexerFactory)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (requestLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requestLineLexerFactory));
            }
            if (statusLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(statusLineLexerFactory));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.requestLineLexerFactory = requestLineLexerFactory;
            this.statusLineLexerFactory = statusLineLexerFactory;
        }

        public ILexer<StartLine> Create()
        {
            return
                new StartLineLexer(
                    alternationLexerFactory.Create(requestLineLexerFactory.Create(), statusLineLexerFactory.Create()));
        }
    }
}
