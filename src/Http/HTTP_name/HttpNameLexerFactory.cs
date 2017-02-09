using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;

namespace Http.HTTP_name
{
    public sealed class HttpNameLexerFactory : RuleLexerFactory<HttpName>
    {
        static HttpNameLexerFactory()
        {
            Default = new HttpNameLexerFactory();
        }

        [NotNull]
        public static HttpNameLexerFactory Default { get; }

        public override ILexer<HttpName> Create()
        {
            var innerLexer = Terminal.Create(@"HTTP", StringComparer.Ordinal);
            return new HttpNameLexer(innerLexer);
        }
    }
}
