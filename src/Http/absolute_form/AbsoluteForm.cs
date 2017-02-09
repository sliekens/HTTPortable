using JetBrains.Annotations;
using UriSyntax.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteForm : AbsoluteUri
    {
        public AbsoluteForm([NotNull] AbsoluteUri absoluteUri)
            : base(absoluteUri)
        {
        }
    }
}