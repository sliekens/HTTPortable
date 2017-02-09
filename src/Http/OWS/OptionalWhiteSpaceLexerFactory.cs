using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.WSP;
using Txt.Core;

namespace Http.OWS
{
    public class OptionalWhiteSpaceLexerFactory : RuleLexerFactory<OptionalWhiteSpace>
    {
        static OptionalWhiteSpaceLexerFactory()
        {
            Default = new OptionalWhiteSpaceLexerFactory(Txt.ABNF.Core.WSP.WhiteSpaceLexerFactory.Default.Singleton());
        }

        public OptionalWhiteSpaceLexerFactory([NotNull] ILexerFactory<WhiteSpace> whiteSpaceLexerFactory)
        {
            if (whiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(whiteSpaceLexerFactory));
            }
            WhiteSpaceLexerFactory = whiteSpaceLexerFactory;
        }

        [NotNull]
        public static OptionalWhiteSpaceLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<WhiteSpace> WhiteSpaceLexerFactory { get; set; }

        public override ILexer<OptionalWhiteSpace> Create()
        {
            var innerLexer = Repetition.Create(WhiteSpaceLexerFactory.Create(), 0, int.MaxValue);
            return new OptionalWhiteSpaceLexer(innerLexer);
        }
    }
}
