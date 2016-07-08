using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.absolute_URI;

namespace Http.absolute_form
{
    public sealed class AbsoluteFormLexer : CompositeLexer<AbsoluteUri, AbsoluteForm>
    {
        public AbsoluteFormLexer([NotNull] ILexer<AbsoluteUri> innerLexer)
            : base(innerLexer)
        {
        }
    }
}
