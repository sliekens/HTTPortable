using System;
using Http.HTTP_version;
using Http.reason_phrase;
using Http.status_code;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.status_line
{
    public class StatusLineLexerFactory : ILexerFactory<StatusLine>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HttpVersion> httpVersionLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly ILexer<ReasonPhrase> reasonPhraseLexer;

        private readonly ILexer<Space> spaceLexer;

        private readonly ILexer<StatusCode> statusCodeLexer;

        public StatusLineLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<HttpVersion> httpVersionLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<StatusCode> statusCodeLexer,
            [NotNull] ILexer<ReasonPhrase> reasonPhraseLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (httpVersionLexer == null)
            {
                throw new ArgumentNullException(nameof(httpVersionLexer));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (statusCodeLexer == null)
            {
                throw new ArgumentNullException(nameof(statusCodeLexer));
            }
            if (reasonPhraseLexer == null)
            {
                throw new ArgumentNullException(nameof(reasonPhraseLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.httpVersionLexer = httpVersionLexer;
            this.spaceLexer = spaceLexer;
            this.statusCodeLexer = statusCodeLexer;
            this.reasonPhraseLexer = reasonPhraseLexer;
            this.newLineLexer = newLineLexer;
        }

        public ILexer<StatusLine> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                httpVersionLexer,
                spaceLexer,
                statusCodeLexer,
                spaceLexer,
                reasonPhraseLexer,
                newLineLexer);
            return new StatusLineLexer(innerLexer);
        }
    }
}
