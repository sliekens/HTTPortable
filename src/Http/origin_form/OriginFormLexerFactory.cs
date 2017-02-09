using System;
using Http.absolute_path;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.query;

namespace Http.origin_form
{
    public sealed class OriginFormLexerFactory : RuleLexerFactory<OriginForm>
    {
        static OriginFormLexerFactory()
        {
            Default = new OriginFormLexerFactory(
                absolute_path.AbsolutePathLexerFactory.Default.Singleton(),
                UriSyntax.query.QueryLexerFactory.Default.Singleton());
        }

        public OriginFormLexerFactory(
            [NotNull] ILexerFactory<AbsolutePath> absolutePathLexerFactory,
            [NotNull] ILexerFactory<Query> queryLexerFactory)
        {
            if (absolutePathLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(absolutePathLexerFactory));
            }
            if (queryLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(queryLexerFactory));
            }
            AbsolutePathLexerFactory = absolutePathLexerFactory;
            QueryLexerFactory = queryLexerFactory;
        }

        [NotNull]
        public static OriginFormLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<AbsolutePath> AbsolutePathLexerFactory { get; set; }

        [NotNull]
        public ILexerFactory<Query> QueryLexerFactory { get; set; }

        public override ILexer<OriginForm> Create()
        {
            var innerLexer = Concatenation.Create(
                AbsolutePathLexerFactory.Create(),
                Option.Create(
                    Concatenation.Create(
                        Terminal.Create(@"?", StringComparer.Ordinal),
                        QueryLexerFactory.Create())));
            return new OriginFormLexer(innerLexer);
        }
    }
}
