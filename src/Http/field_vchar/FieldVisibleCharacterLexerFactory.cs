using System;
using Http.obs_text;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.VCHAR;

namespace Http.field_vchar
{
    public class FieldVisibleCharacterLexerFactory : ILexerFactory<FieldVisibleCharacter>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<ObsoleteText> obsoleteTextLexer;

        private readonly ILexer<VisibleCharacter> visibleCharacterLexer;

        public FieldVisibleCharacterLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] ILexer<VisibleCharacter> visibleCharacterLexer,
            [NotNull] ILexer<ObsoleteText> obsoleteTextLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (visibleCharacterLexer == null)
            {
                throw new ArgumentNullException(nameof(visibleCharacterLexer));
            }
            if (obsoleteTextLexer == null)
            {
                throw new ArgumentNullException(nameof(obsoleteTextLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.visibleCharacterLexer = visibleCharacterLexer;
            this.obsoleteTextLexer = obsoleteTextLexer;
        }

        public ILexer<FieldVisibleCharacter> Create()
        {
            return
                new FieldVisibleCharacterLexer(alternationLexerFactory.Create(visibleCharacterLexer, obsoleteTextLexer));
        }
    }
}
