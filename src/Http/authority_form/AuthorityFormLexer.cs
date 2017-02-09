using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Txt.Core;
using UriSyntax.authority;

namespace Http.authority_form
{
    public class AuthorityFormLexer : Lexer<AuthorityForm>
    {
        public AuthorityFormLexer([NotNull] ILexer<Authority> innerLexer)
        {
            if (innerLexer == null)
            {
                throw new ArgumentNullException(nameof(innerLexer));
            }
            InnerLexer = innerLexer;
        }

        [NotNull]
        public ILexer<Authority> InnerLexer { get; }

        protected override IEnumerable<AuthorityForm> ReadImpl(
            ITextScanner scanner,
            ITextContext context)
        {
            foreach (var authority in InnerLexer.Read(scanner, context))
            {
                yield return new AuthorityForm(authority);
            }
        }
    }
}
