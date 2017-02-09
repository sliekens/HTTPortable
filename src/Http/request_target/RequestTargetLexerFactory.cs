using System;
using Http.absolute_form;
using Http.asterisk_form;
using Http.authority_form;
using Http.origin_form;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.request_target
{
    public sealed class RequestTargetLexerFactory : RuleLexerFactory<RequestTarget>
    {
        static RequestTargetLexerFactory()
        {
            Default = new RequestTargetLexerFactory(
                origin_form.OriginFormLexerFactory.Default.Singleton(),
                absolute_form.AbsoluteFormLexerFactory.Default.Singleton(),
                authority_form.AuthorityFormLexerFactory.Default.Singleton(),
                asterisk_form.AsteriskFormLexerFactory.Default.Singleton());
        }

        public RequestTargetLexerFactory(
            [NotNull] ILexerFactory<OriginForm> originFormLexerFactory,
            [NotNull] ILexerFactory<AbsoluteForm> absoluteFormLexerFactory,
            [NotNull] ILexerFactory<AuthorityForm> authorityFormLexerFactory,
            [NotNull] ILexerFactory<AsteriskForm> asteriskFormLexerFactory)
        {
            if (originFormLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(originFormLexerFactory));
            }
            if (absoluteFormLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(absoluteFormLexerFactory));
            }
            if (authorityFormLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(authorityFormLexerFactory));
            }
            if (asteriskFormLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(asteriskFormLexerFactory));
            }
            OriginFormLexerFactory = originFormLexerFactory;
            AbsoluteFormLexerFactory = absoluteFormLexerFactory;
            AuthorityFormLexerFactory = authorityFormLexerFactory;
            AsteriskFormLexerFactory = asteriskFormLexerFactory;
        }

        [NotNull]
        public static RequestTargetLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<AbsoluteForm> AbsoluteFormLexerFactory { get; }

        [NotNull]
        public ILexerFactory<AsteriskForm> AsteriskFormLexerFactory { get; }

        [NotNull]
        public ILexerFactory<AuthorityForm> AuthorityFormLexerFactory { get; }

        [NotNull]
        public ILexerFactory<OriginForm> OriginFormLexerFactory { get; }

        public override ILexer<RequestTarget> Create()
        {
            var innerLexer = Alternation.Create(
                OriginFormLexerFactory.Create(),
                AbsoluteFormLexerFactory.Create(),
                AuthorityFormLexerFactory.Create(),
                AsteriskFormLexerFactory.Create());
            return new RequestTargetLexer(innerLexer);
        }
    }
}
