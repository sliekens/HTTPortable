using System;
using Http.field_vchar;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;
using Txt.Core;

namespace Http.field_content
{
    public sealed class FieldContentLexerFactory : RuleLexerFactory<FieldContent>
    {
        static FieldContentLexerFactory()
        {
            Default = new FieldContentLexerFactory(
                field_vchar.FieldVisibleCharacterLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.SP.SpaceLexerFactory.Default.Singleton(),
                Txt.ABNF.Core.HTAB.HorizontalTabLexerFactory.Default.Singleton());
        }

        public FieldContentLexerFactory(
            [NotNull] ILexerFactory<FieldVisibleCharacter> fieldVisibleCharacterLexerFactory,
            [NotNull] ILexerFactory<Space> spaceLexerFactory,
            [NotNull] ILexerFactory<HorizontalTab> horizontalTabLexerFactory)
        {
            if (fieldVisibleCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fieldVisibleCharacterLexerFactory));
            }
            if (spaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(spaceLexerFactory));
            }
            if (horizontalTabLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(horizontalTabLexerFactory));
            }
            FieldVisibleCharacterLexerFactory = fieldVisibleCharacterLexerFactory;
            SpaceLexerFactory = spaceLexerFactory;
            HorizontalTabLexerFactory = horizontalTabLexerFactory;
        }

        [NotNull]
        public static FieldContentLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<FieldVisibleCharacter> FieldVisibleCharacterLexerFactory { get; }

        [NotNull]
        public ILexerFactory<HorizontalTab> HorizontalTabLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Space> SpaceLexerFactory { get; }

        public override ILexer<FieldContent> Create()
        {
            var fieldVChar = FieldVisibleCharacterLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                fieldVChar,
                Option.Create(
                    Concatenation.Create(
                        Repetition.Create(
                            Alternation.Create(
                                SpaceLexerFactory.Create(),
                                HorizontalTabLexerFactory.Create()),
                            1,
                            int.MaxValue),
                        fieldVChar)));
            return new FieldContentLexer(innerLexer);
        }
    }
}
