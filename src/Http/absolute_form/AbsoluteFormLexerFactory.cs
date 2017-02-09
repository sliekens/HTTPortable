using System;
using JetBrains.Annotations;
using Txt.ABNF;
using Txt.Core;
using UriSyntax.absolute_URI;

namespace Http.absolute_form
{
    public class AbsoluteFormLexerFactory : RuleLexerFactory<AbsoluteForm>
    {
        static AbsoluteFormLexerFactory()
        {
            Default = new AbsoluteFormLexerFactory(AbsoluteUriLexerFactory.Default.Singleton());
        }

        public AbsoluteFormLexerFactory([NotNull] ILexerFactory<AbsoluteUri> absoluteUri)
        {
            if (absoluteUri == null)
            {
                throw new ArgumentNullException(nameof(absoluteUri));
            }
            AbsoluteUri = absoluteUri;
        }

        [NotNull]
        public static AbsoluteFormLexerFactory Default { get; }

        [NotNull]
        public ILexerFactory<AbsoluteUri> AbsoluteUri { get; }

        public override ILexer<AbsoluteForm> Create()
        {
            var innerLexer = AbsoluteUri.Create();
            return new AbsoluteFormLexer(innerLexer);
        }
    }
}
