using System;
using Http.HTTP_version;
using Http.method;
using Http.request_target;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.SP;

namespace Http.request_line
{
    public class RequestLineLexerFactory : ILexerFactory<RequestLine>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<HttpVersion> httpVersionLexer;

        private readonly ILexer<Method> methodLexer;

        private readonly ILexer<NewLine> newLineLexer;

        private readonly ILexer<RequestTarget> requestTargetLexer;

        private readonly ILexer<Space> spaceLexer;

        public RequestLineLexerFactory(
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<Method> methodLexer,
            [NotNull] ILexer<Space> spaceLexer,
            [NotNull] ILexer<RequestTarget> requestTargetLexer,
            [NotNull] ILexer<HttpVersion> httpVersionLexer,
            [NotNull] ILexer<NewLine> newLineLexer)
        {
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (methodLexer == null)
            {
                throw new ArgumentNullException(nameof(methodLexer));
            }
            if (spaceLexer == null)
            {
                throw new ArgumentNullException(nameof(spaceLexer));
            }
            if (requestTargetLexer == null)
            {
                throw new ArgumentNullException(nameof(requestTargetLexer));
            }
            if (httpVersionLexer == null)
            {
                throw new ArgumentNullException(nameof(httpVersionLexer));
            }
            if (newLineLexer == null)
            {
                throw new ArgumentNullException(nameof(newLineLexer));
            }
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.methodLexer = methodLexer;
            this.spaceLexer = spaceLexer;
            this.requestTargetLexer = requestTargetLexer;
            this.httpVersionLexer = httpVersionLexer;
            this.newLineLexer = newLineLexer;
        }

        public ILexer<RequestLine> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                methodLexer,
                spaceLexer,
                requestTargetLexer,
                spaceLexer,
                httpVersionLexer,
                newLineLexer);
            return new RequestLineLexer(innerLexer);
        }
    }
}
