using System;
using Http.field_name;
using Http.field_value;
using Http.OWS;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.header_field
{
    public sealed class HeaderFieldLexerFactory : RuleLexerFactory<HeaderField>
    {
        static HeaderFieldLexerFactory()
        {
            Default = new HeaderFieldLexerFactory(
                field_name.FieldNameLexerFactory.Default.Singleton(),
                OWS.OptionalWhiteSpaceLexerFactory.Default.Singleton(),
                field_value.FieldValueLexerFactory.Default.Singleton());
        }

        public HeaderFieldLexerFactory(
            [NotNull] ILexerFactory<FieldName> fieldNameLexerFactory,
            [NotNull] ILexerFactory<OptionalWhiteSpace> optionalWhiteSpaceLexerFactory,
            [NotNull] ILexerFactory<FieldValue> fieldValueLexerFactory)
        {
            if (fieldNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fieldNameLexerFactory));
            }
            if (optionalWhiteSpaceLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexerFactory));
            }
            if (fieldValueLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fieldValueLexerFactory));
            }
            FieldNameLexerFactory = fieldNameLexerFactory;
            OptionalWhiteSpaceLexerFactory = optionalWhiteSpaceLexerFactory;
            FieldValueLexerFactory = fieldValueLexerFactory;
        }

        [NotNull]
        public static HeaderFieldLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<FieldName> FieldNameLexerFactory { get; }

        [NotNull]
        public ILexerFactory<FieldValue> FieldValueLexerFactory { get; }

        [NotNull]
        public ILexerFactory<OptionalWhiteSpace> OptionalWhiteSpaceLexerFactory { get; }

        public override ILexer<HeaderField> Create()
        {
            var ows = OptionalWhiteSpaceLexerFactory.Create();
            var innerLexer = Concatenation.Create(
                FieldNameLexerFactory.Create(),
                Terminal.Create(@":", StringComparer.Ordinal),
                ows,
                FieldValueLexerFactory.Create(),
                ows);
            return new HeaderFieldLexer(innerLexer);
        }
    }
}
