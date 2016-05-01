using Http.field_vchar;
using Txt;
using Txt.ABNF;
using Txt.ABNF.Core.HTAB;
using Txt.ABNF.Core.SP;

namespace Http.field_content
{
    public class FieldContentLexerFactory : ILexerFactory<FieldContent>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<FieldVisibleCharacter> fieldVisibleCharacterLexer;

        private readonly ILexer<HorizontalTab> horizontalTabLexer;

        private readonly IOptionLexerFactory optionLexerFactory;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        private readonly ILexer<Space> spaceLexer;

        public FieldContentLexerFactory(
            IAlternationLexerFactory alternationLexerFactory,
            IConcatenationLexerFactory concatenationLexerFactory,
            IRepetitionLexerFactory repetitionLexerFactory,
            IOptionLexerFactory optionLexerFactory,
            ILexer<FieldVisibleCharacter> fieldVisibleCharacterLexer,
            ILexer<Space> spaceLexer,
            ILexer<HorizontalTab> horizontalTabLexer)
        {
            this.alternationLexerFactory = alternationLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.optionLexerFactory = optionLexerFactory;
            this.fieldVisibleCharacterLexer = fieldVisibleCharacterLexer;
            this.spaceLexer = spaceLexer;
            this.horizontalTabLexer = horizontalTabLexer;
        }

        public ILexer<FieldContent> Create()
        {
            return
                new FieldContentLexer(
                    concatenationLexerFactory.Create(
                        fieldVisibleCharacterLexer,
                        optionLexerFactory.Create(
                            concatenationLexerFactory.Create(
                                repetitionLexerFactory.Create(
                                    alternationLexerFactory.Create(spaceLexer, horizontalTabLexer),
                                    1,
                                    int.MaxValue),
                                fieldVisibleCharacterLexer))));
        }
    }
}
