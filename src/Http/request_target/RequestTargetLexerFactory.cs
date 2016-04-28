using System;
using Http.absolute_form;
using Http.asterisk_form;
using Http.authority_form;
using Http.origin_form;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;

namespace Http.request_target
{
    public class RequestTargetLexerFactory : ILexerFactory<RequestTarget>
    {
        private readonly ILexer<AbsoluteForm> absoluteFormLexer;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<AsteriskForm> asteriskFormLexer;

        private readonly ILexer<AuthorityForm> authorityFormLexer;

        private readonly ILexer<OriginForm> originFormLexer;

        public RequestTargetLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<OriginForm> originFormLexer,
            [NotNull] ILexer<AbsoluteForm> absoluteFormLexer,
            [NotNull] ILexer<AuthorityForm> authorityFormLexer,
            [NotNull] ILexer<AsteriskForm> asteriskFormLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (originFormLexer == null)
            {
                throw new ArgumentNullException(nameof(originFormLexer));
            }
            if (absoluteFormLexer == null)
            {
                throw new ArgumentNullException(nameof(absoluteFormLexer));
            }
            if (authorityFormLexer == null)
            {
                throw new ArgumentNullException(nameof(authorityFormLexer));
            }
            if (asteriskFormLexer == null)
            {
                throw new ArgumentNullException(nameof(asteriskFormLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.originFormLexer = originFormLexer;
            this.absoluteFormLexer = absoluteFormLexer;
            this.authorityFormLexer = authorityFormLexer;
            this.asteriskFormLexer = asteriskFormLexer;
        }

        public ILexer<RequestTarget> Create()
        {
            var innerLexer = alternationLexerFactory.Create(originFormLexer, absoluteFormLexer, authorityFormLexer, asteriskFormLexer);
            return new RequestTargetLexer(innerLexer);
        }
    }
}
