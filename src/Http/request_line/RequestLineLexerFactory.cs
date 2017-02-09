using System;
using Http.HTTP_version;
using Http.method;
using Http.request_target;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.request_line
{
    public sealed class RequestLineLexerFactory : RuleLexerFactory<RequestLine>
    {
        static RequestLineLexerFactory()
        {
            Default = new RequestLineLexerFactory(
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton(),
                method.MethodLexerFactory.Default.Singleton(),
                request_target.RequestTargetLexerFactory.Default.Singleton(),
                HTTP_version.HttpVersionLexerFactory.Default.Singleton());
        }

        public RequestLineLexerFactory(
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory,
            [NotNull] ILexerFactory<Method> methodLexerFactory,
            [NotNull] ILexerFactory<RequestTarget> requestTargetLexerFactory,
            [NotNull] ILexerFactory<HttpVersion> httpVersionLexerFactory)
        {
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            if (methodLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(methodLexerFactory));
            }
            if (requestTargetLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requestTargetLexerFactory));
            }
            if (httpVersionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(httpVersionLexerFactory));
            }
            SpaceLexerFactory = spaceLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
            MethodLexerFactory = methodLexerFactory;
            RequestTargetLexerFactory = requestTargetLexerFactory;
            HttpVersionLexerFactory = httpVersionLexerFactory;
        }

        [NotNull]
        public static RequestLineLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HttpVersion> HttpVersionLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Method> MethodLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RequestTarget> RequestTargetLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        public override ILexer<RequestLine> Create()
        {
            var sp = SpaceLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                MethodLexerFactory.Create(),
                sp,
                RequestTargetLexerFactory.Create(),
                sp,
                HttpVersionLexerFactory.Create(),
                NewLineLexerFactory.Create());
            return new RequestLineLexer(innerLexer);
        }
    }
}
