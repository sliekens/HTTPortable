using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.authority;
using UriSyntax.fragment;
using UriSyntax.path_abempty;
using UriSyntax.query;

namespace Http.http_URI
{
    public class HttpUriLexerFactory : ILexerFactory<HttpUri>
    {
        private readonly ILexer<Authority> authorityLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<Fragment> fragmentLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<PathAbsoluteOrEmpty> pathAbsoluteOrEmptyLexer;

        private readonly ILexer<Query> queryLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HttpUriLexerFactory(
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

        public ILexer<HttpUri> Create()
        {
            var innerLexer =
                concatenationLexerFactory.Create(
                    terminalLexerFactory.Create(@"http://", StringComparer.OrdinalIgnoreCase),
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
            return new HttpUriLexer(innerLexer);
        }
    }
}
