using System;
using Http.field_content;
using Http.obs_fold;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_value
{
    public class FieldValueLexerFactory : ILexerFactory<FieldValue>
    {
        private readonly IAlternationLexerFactory alternationLexerFactory;

        private readonly ILexer<FieldContent> fieldContentLexer;

        private readonly ILexer<ObsoleteFold> obsoleteFoldLexer;

        private readonly IRepetitionLexerFactory repetitionLexerFactory;

        public FieldValueLexerFactory(
            [NotNull] IAlternationLexerFactory alternationLexerFactory,
            [NotNull] IRepetitionLexerFactory repetitionLexerFactory,
            [NotNull] ILexer<FieldContent> fieldContentLexer,
            [NotNull] ILexer<ObsoleteFold> obsoleteFoldLexer)
        {
            if (alternationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(alternationLexerFactory));
            }
            if (repetitionLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(repetitionLexerFactory));
            }
            if (fieldContentLexer == null)
            {
                throw new ArgumentNullException(nameof(fieldContentLexer));
            }
            if (obsoleteFoldLexer == null)
            {
                throw new ArgumentNullException(nameof(obsoleteFoldLexer));
            }
            this.alternationLexerFactory = alternationLexerFactory;
            this.repetitionLexerFactory = repetitionLexerFactory;
            this.fieldContentLexer = fieldContentLexer;
            this.obsoleteFoldLexer = obsoleteFoldLexer;
        }

        public ILexer<FieldValue> Create()
        {
            return
                new FieldValueLexer(
                    repetitionLexerFactory.Create(
                        alternationLexerFactory.Create(fieldContentLexer, obsoleteFoldLexer),
                        0,
                        int.MaxValue));
        }
    }
}
