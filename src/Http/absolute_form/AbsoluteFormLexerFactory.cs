using System;
using JetBrains.Annotations;
using Txt;
using Txt.Core;
using UriSyntax.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteFormLexerFactory : ILexerFactory<AbsoluteForm>
    {
        private readonly ILexer<AbsoluteUri> absoluteUriLexer;

        public AbsoluteFormLexerFactory([NotNull] ILexer<AbsoluteUri> absoluteUriLexer)
        {
            if (absoluteUriLexer == null)
            {
                throw new ArgumentNullException(nameof(absoluteUriLexer));
            }
            this.absoluteUriLexer = absoluteUriLexer;
        }

        public ILexer<AbsoluteForm> Create()
        {
            return new AbsoluteFormLexer(absoluteUriLexer);
        }
    }
}
