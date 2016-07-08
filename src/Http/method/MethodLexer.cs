using Http.token;
using JetBrains.Annotations;
using Txt.Core;

namespace Http.method
{
    public sealed class MethodLexer : CompositeLexer<Token, Method>
    {
        public MethodLexer([NotNull] ILexer<Token> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
