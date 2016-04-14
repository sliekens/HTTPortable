using System;
using Txt;
using Uri.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteFormLexerFactory : ILexerFactory<AbsoluteForm>
    {
        private readonly ILexerFactory<AbsoluteUri> absoluteUriLexerFactory;

        public AbsoluteFormLexerFactory(ILexerFactory<AbsoluteUri> absoluteUriLexerFactory)
        {
            if (absoluteUriLexerFactory == null)
            {
                throw new ArgumentNullException(nameof(absoluteUriLexerFactory));
            }

            this.absoluteUriLexerFactory = absoluteUriLexerFactory;
        }

        public ILexer<AbsoluteForm> Create()
        {
            var innerLexer = absoluteUriLexerFactory.Create();
            return new AbsoluteFormLexer(innerLexer);
        }
    }
}