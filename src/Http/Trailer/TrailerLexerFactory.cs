using System;
using Http.field_name;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.Trailer
{
    public sealed class TrailerLexerFactory : RuleLexerFactory<Trailer>
    {
        static TrailerLexerFactory()
        {
            Default = new TrailerLexerFactory(
                Http.RequiredDelimitedListLexerFactory.Default,
                field_name.FieldNameLexerFactory.Default.Singleton());
        }

        public TrailerLexerFactory(
            [NotNull] IRequiredDelimitedListLexerFactory requiredDelimitedListLexerFactory,
            [NotNull] ILexerFactory<FieldName> fieldNameLexerFactory)
        {
            if (requiredDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(requiredDelimitedListLexerFactory));
            }
            if (fieldNameLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(fieldNameLexerFactory));
            }
            RequiredDelimitedListLexerFactory = requiredDelimitedListLexerFactory;
            FieldNameLexerFactory = fieldNameLexerFactory;
        }

        [NotNull]
        public static TrailerLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<FieldName> FieldNameLexerFactory { get; }

        [NotNull]
        public IRequiredDelimitedListLexerFactory RequiredDelimitedListLexerFactory { get; }

        public override ILexer<Trailer> Create()
        {
            var innerLexer = RequiredDelimitedListLexerFactory.Create(FieldNameLexerFactory.Create());
            return new TrailerLexer(innerLexer);
        }
    }
}
