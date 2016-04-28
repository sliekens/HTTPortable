using System;
using Http.absolute_path;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Uri.query;

namespace Http.origin_form
{
    public class OriginFormLexerFactory : ILexerFactory<OriginForm>
    {
        private readonly ILexer<AbsolutePath> absolutePathLexer;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly ILexer<Query> queryLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public OriginFormLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] IOptionLexerFactory optionLexerFactory,
            [NotNull] ILexer<AbsolutePath> absolutePathLexer,
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
            if (optionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionLexerFactory));
            }
            if (absolutePathLexer == null)
            {
                throw new ArgumentNullException(nameof(absolutePathLexer));
            }
            if (queryLexer == null)
            {
                throw new ArgumentNullException(nameof(queryLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.absolutePathLexer = absolutePathLexer;
            this.queryLexer = queryLexer;
        }

        public ILexer<OriginForm> Create()
        {
            var innerLexer = concatenationLexerFactory.Create(
                absolutePathLexer,
                optionLexerFactory.Create(
                    concatenationLexerFactory.Create(
                        terminalLexerFactory.Create(@"?", StringComparer.Ordinal),
                        queryLexer)));
            return new OriginFormLexer(innerLexer);
        }
    }
}
