using System;
using Http.field_name;
using Http.field_value;
using Http.OWS;
using JetBrains.Annotations;
using Txt;
using Txt.ABNF;
using Txt.Core;

namespace Http.header_field
{
    public class HeaderFieldLexerFactory : ILexerFactory<HeaderField>
    {
        private readonly IConcatenationLexerFactory concatenationLexerFactory;

        private readonly ILexer<FieldName> fieldNameLexer;

        private readonly ILexer<FieldValue> fieldValueLexer;

        private readonly ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer;

        private readonly ITerminalLexerFactory terminalLexerFactory;

        public HeaderFieldLexerFactory(
            [NotNull] ITerminalLexerFactory terminalLexerFactory,
            [NotNull] IConcatenationLexerFactory concatenationLexerFactory,
            [NotNull] ILexer<FieldName> fieldNameLexer,
            [NotNull] ILexer<OptionalWhiteSpace> optionalWhiteSpaceLexer,
            [NotNull] ILexer<FieldValue> fieldValueLexer)
        {
            if (terminalLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(terminalLexerFactory));
            }
            if (concatenationLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(concatenationLexerFactory));
            }
            if (fieldNameLexer == null)
            {
                throw new ArgumentNullException(nameof(fieldNameLexer));
            }
            if (optionalWhiteSpaceLexer == null)
            {
                throw new ArgumentNullException(nameof(optionalWhiteSpaceLexer));
            }
            if (fieldValueLexer == null)
            {
                throw new ArgumentNullException(nameof(fieldValueLexer));
            }
            this.terminalLexerFactory = terminalLexerFactory;
            this.concatenationLexerFactory = concatenationLexerFactory;
            this.fieldNameLexer = fieldNameLexer;
            this.optionalWhiteSpaceLexer = optionalWhiteSpaceLexer;
            this.fieldValueLexer = fieldValueLexer;
        }

        public ILexer<HeaderField> Create()
        {
            return
                new HeaderFieldLexer(
                    concatenationLexerFactory.Create(
                        fieldNameLexer,
                        terminalLexerFactory.Create(@":", StringComparer.Ordinal),
                        optionalWhiteSpaceLexer,
                        fieldValueLexer,
                        optionalWhiteSpaceLexer));
        }
    }
}
