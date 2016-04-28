using System;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Http.tchar
{
    public class TokenCharacterLexerFactory : ILexerFactory<TokenCharacter>
    {
        private readonly ILexer<Alpha> alphaLexer;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<Digit> digitLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public TokenCharacterLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<Digit> digitLexer,
            [NotNull] ILexer<Alpha> alphaLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (digitLexer == null)
            {
                throw new ArgumentNullException(nameof(digitLexer));
            }
            if (alphaLexer == null)
            {
                throw new ArgumentNullException(nameof(alphaLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
            this.digitLexer = digitLexer;
            this.alphaLexer = alphaLexer;
        }

        public ILexer<TokenCharacter> Create()
        {
            var innerLexer =
                alternationLexerFactory.Create(
                    terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"^", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"`", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"|", StringComparer.Ordinal),
                    terminalLexerFactory.Create(@"~", StringComparer.Ordinal),
                    digitLexer,
                    alphaLexer);
            return new TokenCharacterLexer(innerLexer);
        }
    }
}
