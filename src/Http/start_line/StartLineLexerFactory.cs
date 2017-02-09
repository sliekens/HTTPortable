using System;
using Http.request_line;
using Http.status_line;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.start_line
{
    public sealed class StartLineLexerFactory : RuleLexerFactory<StartLine>
    {
        static StartLineLexerFactory()
        {
            Default = new StartLineLexerFactory(
                request_line.RequestLineLexerFactory.Default.Singleton(),
                status_line.StatusLineLexerFactory.Default.Singleton());
        }

        public StartLineLexerFactory(
            [NotNull] ILexerFactory<RequestLine> requestLineLexerFactory,
            [NotNull] ILexerFactory<StatusLine> statusLineLexerFactory)
        {
            if (requestLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requestLineLexerFactory));
            }
            if (statusLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(statusLineLexerFactory));
            }
            RequestLineLexerFactory = requestLineLexerFactory;
            StatusLineLexerFactory = statusLineLexerFactory;
        }

        [NotNull]
        public static StartLineLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<RequestLine> RequestLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<StatusLine> StatusLineLexerFactory { get; }

        public override ILexer<StartLine> Create()
        {
            var innerLexer = Alternation.Create(RequestLineLexerFactory.Create(), StatusLineLexerFactory.Create());
            return new StartLineLexer(innerLexer);
        }
    }
}
