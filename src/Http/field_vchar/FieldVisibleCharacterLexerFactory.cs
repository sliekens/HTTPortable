using System;
using Http.obs_text;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.ABNF.Core.VCHAR;
using Txt.Core;

namespace Http.field_vchar
{
    public sealed class FieldVisibleCharacterLexerFactory : RuleLexerFactory<FieldVisibleCharacter>
    {
        static FieldVisibleCharacterLexerFactory()
        {
            Default =
                new FieldVisibleCharacterLexerFactory(
                    Txt.ABNF.Core.VCHAR.VisibleCharacterLexerFactory.Default.Singleton(),
                    obs_text.ObsoleteTextLexerFactory.Default.Singleton());
        }

        public FieldVisibleCharacterLexerFactory(
            [NotNull] ILexerFactory<VisibleCharacter> visibleCharacterLexerFactory,
            [NotNull] ILexerFactory<ObsoleteText> obsoleteTextLexerFactory)
        {
            if (visibleCharacterLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(visibleCharacterLexerFactory));
            }
            if (obsoleteTextLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexerFactory));
            }
            VisibleCharacterLexerFactory = visibleCharacterLexerFactory;
            ObsoleteTextLexerFactory = obsoleteTextLexerFactory;
        }

        [NotNull]
        public static FieldVisibleCharacterLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<ObsoleteText> ObsoleteTextLexerFactory { get; }

        [NotNull]
        public ILexerFactory<VisibleCharacter> VisibleCharacterLexerFactory { get; }

        public override ILexer<FieldVisibleCharacter> Create()
        {
            var innerLexer = Alternation.Create(
                VisibleCharacterLexerFactory.Create(),
                ObsoleteTextLexerFactory.Create());
            return new FieldVisibleCharacterLexer(innerLexer);
        }
    }
}
