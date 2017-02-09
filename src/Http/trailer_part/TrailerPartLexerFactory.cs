using System;
using Http.header_field;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.CRLF;
using Txt.Core;

namespace Http.trailer_part
{
    public sealed class TrailerPartLexerFactory : RuleLexerFactory<TrailerPart>
    {
        static TrailerPartLexerFactory()
        {
            Default = new TrailerPartLexerFactory(
                header_field.HeaderFieldLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.CRLF.NewLineLexerFactory.Default.Singleton());
        }

        public TrailerPartLexerFactory(
            [NotNull] ILexerFactory<HeaderField> headerFieldLexerFactory,
            [NotNull] ILexerFactory<NewLine> newLineLexerFactory)
        {
            if (headerFieldLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(headerFieldLexerFactory));
            }
            if (newLineLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(newLineLexerFactory));
            }
            HeaderFieldLexerFactory = headerFieldLexerFactory;
            NewLineLexerFactory = newLineLexerFactory;
        }

        [NotNull]
        public static TrailerPartLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<HeaderField> HeaderFieldLexerFactory { get; }

        [NotNull]
        public ILexerFactory<NewLine> NewLineLexerFactory { get; }

        public override ILexer<TrailerPart> Create()
        {
            var innerLexer =
                Repetition.Create(
                    Concatenation.Create(HeaderFieldLexerFactory.Create(), NewLineLexerFactory.Create()),
                    0,
                    int.MaxValue);
            return new TrailerPartLexer(innerLexer);
        }
    }
}
