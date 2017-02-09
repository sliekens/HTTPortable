using System;
using Http.field_content;
using Http.obs_fold;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.field_value
{
    public sealed class FieldValueLexerFactory : RuleLexerFactory<FieldValue>
    {
        static FieldValueLexerFactory()
        {
            Default = new FieldValueLexerFactory(
                field_content.FieldContentLexerFactory.Default.Singleton(),
                obs_fold.ObsoleteFoldLexerFactory.Default.Singleton());
        }

        public FieldValueLexerFactory(
            [NotNull] ILexerFactory<FieldContent> fieldContentLexerFactory,
            [NotNull] ILexerFactory<ObsoleteFold> obsoleteFoldLexerFactory)
        {
            if (fieldContentLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fieldContentLexerFactory));
            }
            if (obsoleteFoldLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(obsoleteFoldLexerFactory));
            }
            FieldContentLexerFactory = fieldContentLexerFactory;
            ObsoleteFoldLexerFactory = obsoleteFoldLexerFactory;
        }

        [NotNull]
        public static FieldValueLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<FieldContent> FieldContentLexerFactory { get; }

        [NotNull]
        public ILexerFactory<ObsoleteFold> ObsoleteFoldLexerFactory { get; }

        public override ILexer<FieldValue> Create()
        {
            var innerLexer = Repetition.Create(
                Alternation.Create(
                    FieldContentLexerFactory.Create(),
                    ObsoleteFoldLexerFactory.Create()),
                0,
                int.MaxValue)
                ;
            return new FieldValueLexer(innerLexer);
        }
    }
}
