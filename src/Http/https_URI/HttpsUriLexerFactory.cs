using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.authority;
using UriSyntax.fragment;
using UriSyntax.path_abempty;
using UriSyntax.query;

namespace Http.https_URI
{
    public sealed class HttpsUriLexerFactory : RuleLexerFactory<HttpsUri>
    {
        static HttpsUriLexerFactory()
        {
            Default = new HttpsUriLexerFactory(
                UriSyntax.authority.AuthorityLexerFactory.Default.Singleton(),
                UriSyntax.path_abempty.PathAbsoluteOrEmptyLexerFactory.Default.Singleton(),
                UriSyntax.query.QueryLexerFactory.Default.Singleton(),
                UriSyntax.fragment.FragmentLexerFactory.Default.Singleton());
        }

        public HttpsUriLexerFactory(
            [NotNull] ILexerFactory<Authority> authorityLexerFactory,
            [NotNull] ILexerFactory<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory,
            [NotNull] ILexerFactory<Fragment> fragmentLexerFactory)
        {
            if (authorityLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(authorityLexerFactory));
            }
            if (pathAbsoluteOrEmptyLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexerFactory));
            }
            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }
            if (fragmentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexerFactory));
            }
            AuthorityLexerFactory = authorityLexerFactory;
            PathAbsoluteOrEmptyLexerFactory = pathAbsoluteOrEmptyLexerFactory;
            QueryLexerFactory = queryLexerFactory;
            FragmentLexerFactory = fragmentLexerFactory;
        }

        [NotNull]
        public static HttpsUriLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Authority> AuthorityLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Fragment> FragmentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<PathAbsoluteOrEmpty> PathAbsoluteOrEmptyLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        public override ILexer<HttpsUri> Create()
        {
            var innerLexer =
                Concatenation.Create(
                    Terminal.Create(@"https://", StringComparer.OrdinalIgnoreCase),
                    AuthorityLexerFactory.Create(),
                    PathAbsoluteOrEmptyLexerFactory.Create(),
                    Option.Create(
                        Concatenation.Create(
                            Terminal.Create(@"?", StringComparer.Ordinal),
                            QueryLexerFactory.Create())),
                    Option.Create(
                        Concatenation.Create(
                            Terminal.Create(@"#", StringComparer.Ordinal),
                            FragmentLexerFactory.Create())));
            return new HttpsUriLexer(innerLexer);
        }
    }
}
