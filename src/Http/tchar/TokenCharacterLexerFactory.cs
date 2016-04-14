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

        private readonly IAlternativeLexerFactory alternativeLexerFactory;

        private readonly ILexerFactory<Digit> digitLexerFactory;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public TokenCharacterLexerFactory(
            ITerminalLexerFactory terminalLexerFactory,
            ILexerFactory<Alpha> alphaLexerFactory,
            ILexerFactory<Digit> digitLexerFactory,
            IAlternativeLexerFactory alternativeLexerFactory)
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

            if (alternativeLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternativeLexerFactory));
            }

            this.terminalLexerFactory = terminalLexerFactory;
            this.alphaLexerFactory = alphaLexerFactory;
            this.digitLexerFactory = digitLexerFactory;
            this.alternativeLexerFactory = alternativeLexerFactory;
        }

        public ILexer<TokenCharacter> Create()
        {
            var innerLexer =
                alternativeLexerFactory.Create(
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