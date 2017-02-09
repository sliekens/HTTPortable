using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.query;
using UriSyntax.relative_part;

namespace Http.partial_URI
{
    public class PartialUriLexerFactory : RuleLexerFactory<PartialUri>
    {
        static PartialUriLexerFactory()
        {
            Default = new PartialUriLexerFactory(
                Http.OptionalDelimitedListLexerFactory.Default,
                UriSyntax.relative_part.RelativePartLexerFactory.Default.Singleton(),
                UriSyntax.query.QueryLexerFactory.Default.Singleton());
        }

        public PartialUriLexerFactory(
            [NotNull] IOptionalDelimitedListLexerFactory optionalDelimitedListLexerFactory,
            [NotNull] ILexerFactory<RelativePart> relativePartLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory)
        {
            if (optionalDelimitedListLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(optionalDelimitedListLexerFactory));
            }
            if (relativePartLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(relativePartLexerFactory));
            }
            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }
            OptionalDelimitedListLexerFactory = optionalDelimitedListLexerFactory;
            RelativePartLexerFactory = relativePartLexerFactory;
            QueryLexerFactory = queryLexerFactory;
        }

        [NotNull]
        public static PartialUriLexerFactory Default { get; }

        [NotNull]
        public IOptionalDelimitedListLexerFactory OptionalDelimitedListLexerFactory { get; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; }

        [NotNull]
        public ILexerFactory<RelativePart> RelativePartLexerFactory { get; }

        public override ILexer<PartialUri> Create()
        {
            var innerLexer = Concatenation.Create(
                RelativePartLexerFactory.Create(),
                OptionalDelimitedListLexerFactory.Create(
                    Concatenation.Create(
                        Terminal.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())));
            return new PartialUriLexer(innerLexer);
        }
    }
}
