using System;
using Http.http_URI;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.authority;
using Uri.fragment;
using Uri.path_abempty;
using Uri.query;

namespace Http.https_URI
{
    public class HttpsUriLexerFactory : ILexerFactory<HttpsUri>
    {
        private readonly ILexer<Authority> authorityLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Fragment> fragmentLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<Query> queryLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpsUriLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<Authority> authorityLexer,
            [NotNull] ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer,
            [NotNull] ILexer<Query> queryLexer,
            [NotNull] ILexer<Fragment> fragmentLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (authorityLexer == null)
            {
                throw new ArgumentNullException(nameof(authorityLexer));
            }
            if (pathAbsoluteOrEmptyLexer == null)
            {
                throw new ArgumentNullException(nameof(pathAbsoluteOrEmptyLexer));
            }
            if (queryLexer == null)
            {
                throw new ArgumentNullException(nameof(queryLexer));
            }
            if (fragmentLexer == null)
            {
                throw new ArgumentNullException(nameof(fragmentLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.authorityLexer = authorityLexer;
            this.pathAbsoluteOrEmptyLexer = pathAbsoluteOrEmptyLexer;
            this.queryLexer = queryLexer;
            this.fragmentLexer = fragmentLexer;
        }

        public ILexer<HttpsUri> Create()
        {
            var innerLexer =
                concatenationLexerFactory.Create(
                    terminalLexerFactory.Create(@"https://", StringComparer.OrdinalIgnoreCase),
                    authorityLexer,
                    pathAbsoluteOrEmptyLexer,
                    optionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                            queryLexer)),
                    optionLexerFactory.Create(
                        concatenationLexerFactory.Create(
                            terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                            fragmentLexer)));
            return new HttpsUriLexer(innerLexer);
        }
    }
}