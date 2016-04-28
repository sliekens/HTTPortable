using System;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;

namespace Http.tchar
{
    public class TokenCharacterLexerFactory : ILexerFactory<TokenCharacter>
    {
        private readonly ILexerFactory<Alpha> alphaLexerFactory;

        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public TokenCharacterLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            IAlternationLexerFactory alternationLexerFactory)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }

            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }

            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }

            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.alternationLexerFactory = alternationLexerFactory;
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
                    digitLexerFactory.Create(),
                    alphaLexerFactory.Create());
            return new TokenCharacterLexer(innerLexer);
        }
    }
}