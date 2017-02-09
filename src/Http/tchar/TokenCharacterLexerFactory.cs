using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.ALPHA;
using Txt.ABNF.Core.DIGIT;
using Txt.Core;

namespace Http.tchar
{
    public class TokenCharacterLexerFactory : RuleLexerFactory<TokenCharacter>
    {
        static TokenCharacterLexerFactory()
        {
            Default = new TokenCharacterLexerFactory(
                Txt.ABNF.Core.DIGIT.DigitLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.ALPHA.AlphaLexerFactory.Default.Singleton());
        }

        public TokenCharacterLexerFactory(
            [NotNull] ILexerFactory<Digit> digitLexerFactory,
            [NotNull] ILexerFactory<Alpha> alphaLexerFactory)
        {
            if (digitLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(digitLexerFactory));
            }
            if (alphaLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alphaLexerFactory));
            }
            DigitLexerFactory = digitLexerFactory;
            AlphaLexerFactory = alphaLexerFactory;
        }

        [NotNull]
        public static TokenCharacterLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<Alpha> AlphaLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Digit> DigitLexerFactory { get; set; }

        public override ILexer<TokenCharacter> Create()
        {
            var innerLexer = Alternation.Create(
                Terminal.Create(@"!", StringComparer.Ordinal),
                Terminal.Create(@"#", StringComparer.Ordinal),
                Terminal.Create(@"$", StringComparer.Ordinal),
                Terminal.Create(@"%", StringComparer.Ordinal),
                Terminal.Create(@"&", StringComparer.Ordinal),
                Terminal.Create(@"'", StringComparer.Ordinal),
                Terminal.Create(@"*", StringComparer.Ordinal),
                Terminal.Create(@"+", StringComparer.Ordinal),
                Terminal.Create(@"-", StringComparer.Ordinal),
                Terminal.Create(@".", StringComparer.Ordinal),
                Terminal.Create(@"^", StringComparer.Ordinal),
                Terminal.Create(@"_", StringComparer.Ordinal),
                Terminal.Create(@"`", StringComparer.Ordinal),
                Terminal.Create(@"|", StringComparer.Ordinal),
                Terminal.Create(@"~", StringComparer.Ordinal),
                DigitLexerFactory.Create(),
                AlphaLexerFactory.Create());
            return new TokenCharacterLexer(innerLexer);
        }
    }
}
