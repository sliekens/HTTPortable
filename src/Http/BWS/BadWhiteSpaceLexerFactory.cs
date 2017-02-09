using System;
using Http.OWS;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.BWS
{
    public class BadWhiteSpaceLexerFactory : RuleLexerFactory<BadWhiteSpace>
    {
        static BadWhiteSpaceLexerFactory()
        {
            Default = new BadWhiteSpaceLexerFactory(OptionalWhiteSpaceLexerFactory.Default.Singleton());
        }

        public BadWhiteSpaceLexerFactory([NotNull] ILexerFactory<OptionalWhiteSpace> optionalWhiteSpace)
        {
            if (optionalWhiteSpace == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpace));
            }
            OptionalWhiteSpace = optionalWhiteSpace;
        }

        [NotNull]
        public static BadWhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<OptionalWhiteSpace> OptionalWhiteSpace { get; }

        public override ILexer<BadWhiteSpace> Create()
        {
            var innerLexer = OptionalWhiteSpace.Create();
            return new BadWhiteSpaceLexer(innerLexer);
        }
    }
}
