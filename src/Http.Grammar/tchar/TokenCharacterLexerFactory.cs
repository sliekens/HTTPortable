namespace Http.Grammar
{
    using System;

    using TextFx;
    using TextFx.ABNF;
    using TextFx.ABNF.Core;

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
                this.alternativeLexerFactory.Create(
                    this.terminalLexerFactory.Create(@"!", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"#", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"$", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"%", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"&", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"'", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"*", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"+", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"-", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@".", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"^", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"_", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"`", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"|", StringComparer.Ordinal),
                    this.terminalLexerFactory.Create(@"~", StringComparer.Ordinal),
                    this.digitLexerFactory.Create(),
                    this.alphaLexerFactory.Create());
            return new TokenCharacterLexer(innerLexer);
        }
    }
}