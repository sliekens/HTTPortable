using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace Http.partial_URI
{
    public class PartialUriLexerFactory : ILexerFactory<PartialUri>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory;

        private readonly ILexer<Query> queryLexer;

        private readonly ILexer<RelativePart> relativePartLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public PartialUriLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory,
            [NotNull] ILexer<RelativePart> relativePartLexer,
            [NotNull] ILexer<Query> queryLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (optionalDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalDelimitedListLexerFactory));
            }
            if (relativePartLexer == null)
            {
                throw new ArgumentNullException(nameof(relativePartLexer));
            }
            if (queryLexer == null)
            {
                throw new ArgumentNullException(nameof(queryLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionalDelimitedListLexerFactory = optionalDelimitedListLexerFactory;
            this.relativePartLexer = relativePartLexer;
            this.queryLexer = queryLexer;
        }

        public ILexer<PartialUri> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                relativePartLexer,
                optionalDelimitedListLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        queryLexer)));
            return new PartialUriLexer(innerLexer);
        }
    }
}
