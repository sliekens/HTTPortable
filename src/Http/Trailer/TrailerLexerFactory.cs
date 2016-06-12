using System;
using Http.field_name;
using JetBrains.Annotations;
using Txt;
using Txt.Core;

namespace Http.Trailer
{
    public class TrailerLexerFactory : ILexerFactory<Trailer>
    {
        private readonly ILexer<FieldName> fieldNameLexer;

        private readonly IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory;

        public TrailerLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexer<FieldName> fieldNameLexer)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (fieldNameLexer == null)
            {
                throw new ArgumentNullException(nameof(fieldNameLexer));
            }
            this.requiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            this.fieldNameLexer = fieldNameLexer;
        }

        public ILexer<Trailer> Create()
        {
            return new TrailerLexer(requiredDelimitedListLexerFactory.Create(fieldNameLexer));
        }
    }
}
