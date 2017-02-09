using System;
using Http.HTTP_version;
using Http.reason_phrase;
using Http.status_code;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.status_line
{
    public sealed class StatusLineLexerFactory : RuleLexerFactory<StatusLine>
    {
        static StatusLineLexerFactory()
        {
            Default = new StatusLineLexerFactory(
                HTTP_version.HttpVersionLexerFactory.Default.Singleton(),
                status_code.StatusCodeLexerFactory.Default.Singleton(),
                reason_phrase.ReasonPhraseLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton());
        }

        public StatusLineLexerFactory(
            [NotNull] ILexerFactory<HttpVersion> httpVersionLexerFactory,
            [NotNull] ILexerFactory<StatusCode> statusCodeLexerFactory,
            [NotNull] ILexerFactory<ReasonPhrase> reasonPhraseLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (httpVersionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(httpVersionLexerFactory));
            }
            if (statusCodeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(statusCodeLexerFactory));
            }
            if (reasonPhraseLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(reasonPhraseLexerFactory));
            }
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            HttpVersionLexerFactory = httpVersionLexerFactory;
            StatusCodeLexerFactory = statusCodeLexerFactory;
            ReasonPhraseLexerFactory = reasonPhraseLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static StatusLineLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HttpVersion> HttpVersionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ReasonPhrase> ReasonPhraseLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        [NotNull]
        public ILexerFactory<StatusCode> StatusCodeLexerFactory { get; }

        public override ILexer<StatusLine> Create()
        {
            var sp = SpaceLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                HttpVersionLexerFactory.Create(),
                sp,
                StatusCodeLexerFactory.Create(),
                sp,
                ReasonPhraseLexerFactory.Create(),
                NewLineLexerFactory.Create());
            return new StatusLineLexer(innerLexer);
        }
    }
}
